
var all_products_json = [];

// open & close menu

var menu = document.querySelector('#menu');

function open_menu() {
    menu.classList.add("active")
}
function close_menu() {
    menu.classList.remove("active")
}


function scrollToTop() {
    window.scrollTo({
        top: 0,
        behavior: 'smooth'
    });
}

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
    if (token) {
        loadCart();
    }
});
    const token = localStorage.getItem('jwtToken');
