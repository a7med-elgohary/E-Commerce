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





// وظائف عربة التسوق
function openCart() {
    if (cart) cart.classList.add('active');
}

function closeCart() {
    if (cart) cart.classList.remove('active');
}

function addToCart(id, btn) {
    const productToAdd = all_products_json.find(product => product.id === id);
    if (productToAdd) {
        product_cart.push(productToAdd);
        if (btn) btn.classList.add("active");
        getCartItems();
    }
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

function remove_from_cart(index) {
    if (index >= 0 && index < product_cart.length) {
        product_cart.splice(index, 1);
        getCartItems();
        updateCartButtons();
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

// تحميل المنتجات عند بدء التشغيل
document.addEventListener('DOMContentLoaded', function () {
    fetch('api/Product/GetAllProducts')
        .then(response => {
            if (!response.ok) throw new Error('Network response was not ok');
            return response.json();
        })
        .then(data => {
            all_products_json = data;
            console.log('Products loaded successfully');
        })
        .catch(error => {
            console.error('Error loading products:', error);
            // يمكنك إضافة عرض رسالة خطأ للمستخدم هنا
        });
});