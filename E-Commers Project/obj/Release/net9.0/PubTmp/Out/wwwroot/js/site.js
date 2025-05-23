// تعريف جميع متغيرات DOM في النطاق العام
var cart = document.querySelector('.cart');
var items_in_cart = document.querySelector(".items_in_cart");
var count_item = document.querySelector('.count_item');
var count_item_cart = document.querySelector('.count_item_cart');
var price_cart_total = document.querySelector('.price_cart_total');
var price_cart_Head = document.querySelector('.price_cart_Head');
var all_products_json = [];
var product_cart = [];

// open & close menu

var menu = document.querySelector('#menu');

function open_menu() {
    menu.classList.add("active")
}
function close_menu() {
    menu.classList.remove("active")
}



document.addEventListener('DOMContentLoaded', () => {
    loadCart();
});


// وظائف عربة التسوق
function openCart() {
    if (cart) cart.classList.add('active');
}

function closeCart() {
    if (cart) cart.classList.remove('active');
}

// إضافة منتج إلى السلة
function addToCart(productId, btn) {
    // التحقق من وجود زر
    if (!btn) return;

    // إظهار مؤشر التحميل
    const originalHTML = btn.innerHTML;
    btn.disabled = true;
    btn.innerHTML = '<i class="fas fa-spinner fa-spin"></i>';

    fetch(`/api/Cart/AddProductToCart/${productId}`, {
        method: 'POST'
    })
    .then(response => {
        if (!response.ok) {
            throw new Error('Network response was not ok');
        }
        return response.json();
    })
    .then(() => {
        // تحديث السلة
        loadCart();
        
        // تحديث زر الإضافة
        btn.classList.add("active");
        btn.disabled = false;
        btn.innerHTML = '<i class="fa-solid fa-cart-plus"></i>';
        
        // إشعار بنجاح الإضافة
        showNotification('تمت إضافة المنتج إلى السلة بنجاح', 'success');
    })
    .catch(error => {
        console.error('Error adding to cart:', error);
        showNotification('حدث خطأ أثناء إضافة المنتج إلى السلة', 'error');
        
        // إعادة الزر إلى حالته الأصلية
        btn.disabled = false;
        btn.innerHTML = originalHTML;
    });
}


// دالة لعرض الإشعارات
function showNotification(message, type = 'info') {
    // يمكنك استخدام مكتبة مثل Toastr أو SweetAlert2
    // أو إنشاء عنصر إشعار مخصص
    const notification = document.createElement('div');
    notification.className = `notification ${type}`;
    notification.textContent = message;
    document.body.appendChild(notification);
    
    setTimeout(() => {
        notification.remove();
    }, 3000);
}

function getCartItems() {
    if (!items_in_cart) {
        console.error('items_in_cart element not found');
        return;
    }

    let total_price = 0;
    let items_c = "";

    product_cart.forEach((product, index) => {
        const productImg = product.photos?.[0]?.url || 'default-image.jpg';

        items_c += `
        <div class="item_cart">
            <img src="${productImg}" alt="${product.name}" onerror="this.src='default-image.jpg'">
            <div class="content">
                <h4>${product.name}</h4>
                <p class="price_cart">$${(product.price || 0).toFixed(2)}</p>
            </div>
            <button class="delete_item" data-index="${index}">
                <i class="fa-solid fa-trash-can"></i>
            </button>
        </div>`;

        total_price += product.price || 0;
    });

    items_in_cart.innerHTML = items_c || '<p>Your cart is empty</p>';

    // تحديث عناصر واجهة المستخدم
    if (price_cart_Head) price_cart_Head.textContent = "$" + total_price.toFixed(2);
    if (count_item) count_item.textContent = product_cart.length;
    if (count_item_cart) count_item_cart.textContent = `(${product_cart.length} Item${product_cart.length !== 1 ? 's' : ''} in Cart)`;
    if (price_cart_total) price_cart_total.textContent = "$" + total_price.toFixed(2);

    // إضافة معالج الأحداث لأزرار الحذف
    document.querySelectorAll('.delete_item').forEach(button => {
        button.addEventListener('click', function () {
            const index = parseInt(this.dataset.index);
            remove_from_cart(index);
        });
    });
}


// تحسين وظيفة حذف المنتج
function remove_from_cart(index) {
    if (index >= 0 && index < product_cart.length) {
        const productId = product_cart[index].id;
        
        // حذف من قاعدة البيانات أولاً
        fetch(`/api/Cart/RemoveProductFromCart/${productId}`, {
            method: 'DELETE'
        })
        .then(response => {
            if (response.ok) {
                // ثم حذف من الواجهة
                product_cart.splice(index, 1);
                updateCartUI();
                updateCartButtons();
            }
        })
        .catch(error => console.error('Error removing item:', error));
    }
}

function updateCartButtons() {
    document.querySelectorAll(".fa-cart-plus").forEach(button => {
        if (!button) return;

        const productId = parseInt(button.closest('.product')?.dataset?.productId);
        if (productId) {
            button.classList.toggle("active", product_cart.some(p => p.id === productId));
        }
    });
}

// تحميل السلة من قاعدة البيانات
function loadCart() {
    // إظهار مؤشر التحميل
    if (items_in_cart) {
        items_in_cart.innerHTML = '<div class="loading-cart">جاري تحميل السلة...</div>';
    }

    fetch('/api/Cart/GetCartItems')
    .then(response => {
        if (!response.ok) {
            throw new Error('Network response was not ok');
        }
        return response.json();
    })
    .then(data => {
        // التأكد من أن البيانات هي مصفوفة
        if (!Array.isArray(data)) {
            console.error('Invalid data format:', data);
            data = [];
        }
        
        // تحديث مصفوفة المنتجات
        product_cart = data;
        
        // تحديث واجهة المستخدم
        updateCartUI();
        
        // تحديث أزرار الإضافة إلى السلة
        updateCartButtons();
    })
    .catch(error => {
        console.error('Failed to load cart items:', error);
        showNotification('حدث خطأ أثناء تحميل محتويات السلة', 'error');
        
        // عرض رسالة خطأ في واجهة المستخدم
        if (items_in_cart) {
            items_in_cart.innerHTML = '<div class="error-message">حدث خطأ أثناء تحميل السلة. يرجى المحاولة مرة أخرى لاحقًا.</div>';
        }
    });
}

function updateCartUI() {
    if (!items_in_cart) {
        console.error('items_in_cart element not found');
        return;
    }

    let total_price = 0;
    let items_c = "";

    product_cart.forEach((product, index) => {
        const productImg = product.photos?.[0]?.url || '/wwwroot/default.jpg';
        
        items_c += `
        <div class="item_cart">
            <img src="${productImg}" alt="${product.name}" onerror="this.src='/wwwroot/default.jpg'">
            <div class="content">
                <h4>${product.name}</h4>
                <p class="price_cart">$${(product.price || 0).toFixed(2)}</p>
            </div>
            <button class="delete_item" data-index="${index}">
                <i class="fa-solid fa-trash-can"></i>
            </button>
        </div>`;

        total_price += product.price || 0;
    });

    items_in_cart.innerHTML = items_c || '<p>Your cart is empty</p>';

    // تحديث عناصر واجهة المستخدم
    if (price_cart_Head) price_cart_Head.textContent = "$" + total_price.toFixed(2);
    if (count_item) count_item.textContent = product_cart.length;
    if (count_item_cart) count_item_cart.textContent = `(${product_cart.length} Item${product_cart.length !== 1 ? 's' : ''} in Cart)`;
    if (price_cart_total) price_cart_total.textContent = "$" + total_price.toFixed(2);

    // إضافة معالج الأحداث لأزرار الحذف
    document.querySelectorAll('.delete_item').forEach(button => {
        button.addEventListener('click', function () {
            const index = parseInt(this.dataset.index);
            if (index >= 0 && index < product_cart.length) {
                // حذف من قاعدة البيانات أولاً
                fetch(`/api/Cart/RemoveProductFromCart/${product_cart[index].id}`, {
                    method: 'DELETE'
                })
                .then(response => {
                    if (response.ok) {
                        // ثم حذف من الواجهة
                        product_cart.splice(index, 1);
                        updateCartUI();
                    }
                })
                .catch(error => console.error('Error removing item:', error));
            }
        });
    });
}
// وظيفة الانتقال إلى الأعلى
function scrollToTop() {
    window.scrollTo({
        top: 0,
        behavior: 'smooth'
    });
}

// ربط زر الـ back_to_top بالوظيفة
document.addEventListener('DOMContentLoaded', function () {
    const backToTopButton = document.querySelector('.back_to_top');
    if (backToTopButton) {
        backToTopButton.addEventListener('click', scrollToTop);
    }

    // إظهار/إخفاء الزر بناءً على موقع المستخدم
    window.addEventListener('scroll', function() {
        if (window.pageYOffset > 300) {
            if (backToTopButton) backToTopButton.style.display = 'block';
        } else {
            if (backToTopButton) backToTopButton.style.display = 'none';
        }
    });

    // تحميل المنتجات من قاعدة البيانات
    // تحميل المنتجات مع JWT token (اختياري)
    const token = localStorage.getItem('jwtToken');
    
    // تحميل المنتجات بدون الحاجة إلى توكن
    fetch('api/Product/Getall')
    .then(response => {
        if (!response.ok) {
            throw new Error('Network response was not ok');
        }
        return response.json();
    })
    .then(data => {
        all_products_json = data;
        // تحديث المنتجات في الواجهة
        updateProductsDisplay();
        
        // حفظ المنتجات في sessionStorage
        sessionStorage.setItem('products', JSON.stringify(data));
    })
    .catch(error => console.error('Error loading products:', error));

    // تحميل السلة إذا كان المستخدم مسجل الدخول
    if (token) {
        loadCart();
    }
});
