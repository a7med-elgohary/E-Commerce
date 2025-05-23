document.addEventListener('DOMContentLoaded', function() {
    // دالة التحقق من تسجيل الدخول
    function checkAuth() {
        const token = localStorage.getItem('jwtToken');
        if (!token) {
            // تخزين الصفحة الحالية للعودة إليها بعد تسجيل الدخول
            sessionStorage.setItem('returnUrl', window.location.pathname);
            window.location.href = '/Auth/AuthScreen';
            return false;
        }
        return true;
    }

    // دالة إضافة المنتج للسلة
    async function addToCart(productId) {
        if (!checkAuth()) return;

        try {
            const response = await fetch(`/api/Cart/AddProductToCart/${productId}`, {
                method: 'POST',
                headers: {
                    'Authorization': `Bearer ${localStorage.getItem('jwtToken')}`
                }
            });

            if (response.status === 401) {
                window.location.href = '/Auth/AuthScreen';
                return;
            }

            if (response.ok) {
                // تحديث عدد المنتجات في السلة
                updateCartCount();
            } else {
                console.error('Failed to add product to cart');
            }
        } catch (error) {
            console.error('Error:', error);
        }
    }

    // دالة تحديث عدد المنتجات في السلة
    async function updateCartCount() {
        if (!checkAuth()) return;

        try {
            const response = await fetch('/api/Cart/GetCartItems', {
                headers: {
                    'Authorization': `Bearer ${localStorage.getItem('jwtToken')}`
                }
            });

            if (response.status === 401) {
                window.location.href = '/Auth/AuthScreen';
                return;
            }

            if (response.ok) {
                const data = await response.json();
                const count = data.length;
                document.querySelector('.cart-count').textContent = count;
            }
        } catch (error) {
            console.error('Error:', error);
        }
    }

    // إضافة مستمعين للأحداث
    document.querySelectorAll('.add-to-cart').forEach(button => {
        button.addEventListener('click', function() {
            const productId = this.dataset.productId;
            addToCart(productId);
        });
    });

    // تحديث عدد المنتجات في السلة عند تحميل الصفحة
    updateCartCount();
});