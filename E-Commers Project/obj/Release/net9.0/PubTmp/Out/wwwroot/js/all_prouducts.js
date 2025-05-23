// تحميل المنتجات
fetch('/api/Product/Getall')
.then(response => {
    if (!response.ok) {
        throw new Error(`HTTP error! status: ${response.status}`);
    }
    return response.json();
})
    .then(data => {
        window.all_products_json = data;
        return data;
    })
    .then(data => {
        const products_dev = document.getElementById("products_dev");
        
        if (!products_dev) {
            console.error('products_dev element not found');
            return;
        }

        data.forEach(product => {
            // Get the first photo as main image
            const mainPhoto = product.photos[0];
            const hoverPhoto = product.photos[1];

            // Normalize image paths
            const mainImageUrl = mainPhoto ? mainPhoto.url.replace(/\\/g, '/') : '';
            const hoverImageUrl = hoverPhoto ? hoverPhoto.url.replace(/\\/g, '/') : '';

            products_dev.innerHTML += `
            <div class="product swiper-slide" >
            <div class="icons">
            <span><a href="#" onclick="addToCart(${product.id}); return false;"><i class="fa-solid fa-cart-plus cart-icon"></i></a></span>
            <span><i class="fa-solid fa-heart"></i></span>
            <span><i class="fa-solid fa-share"></i></span>
            </div>
            
            <a href="/Home/Prouduct/${product.id}">
                    <div class="img_product">
                        <img src="${mainImageUrl}" alt="${product.name}">
                        ${hoverImageUrl ? `<img class="img_hover" src="${hoverImageUrl}" alt="${product.name} - hover">` : ''}
                    </div>
                    
                    <h3 class="name_product"><a href="#">${product.name}</a></h3>

                    <div class="stars">
                        <i class="fa-solid fa-star"></i>
                        <i class="fa-solid fa-star"></i>
                        <i class="fa-solid fa-star"></i>
                        <i class="fa-solid fa-star"></i>
                        <i class="fa-solid fa-star"></i>
                    </div>

                    <div class="price">
                        <p><span>$${(product.price / 100).toFixed(2)}</span></p>
                    </div>
                </div>
            </a>
            `;
        });
    })
    .catch(error => {
        console.error('Error fetching products:', error);
    });
// Star rating generator
function generateStarRating(rating) {
    const fullStars = Math.floor(rating);
    const hasHalfStar = rating % 1 >= 0.5;
    let starsHtml = '';

    for (let i = 1; i <= 5; i++) {
        if (i <= fullStars) {
            starsHtml += '<i class="fa-solid fa-star"></i>';
        } else if (i === fullStars + 1 && hasHalfStar) {
            starsHtml += '<i class="fa-solid fa-star-half-alt"></i>';
        } else {
            starsHtml += '<i class="fa-regular fa-star"></i>';
        }
    }

    return starsHtml;
}

// Event listeners setup
function setupProductEventListeners() {
    document.addEventListener('click', function (e) {
        // Add to cart
        if (e.target.classList.contains('cart-icon')) {
            const productElement = e.target.closest('.product');
            const productId = parseInt(productElement?.dataset?.productId);

            if (productId) {
                // 1. أضف المنتج للواجهة
                addToCart(productId, e.target);
            }
        }

        // Add to wishlist
        if (e.target.classList.contains('wishlist-icon')) {
            e.target.classList.toggle('active');
            // Add wishlist logic here
        }

        // Share product
        if (e.target.classList.contains('share-icon')) {
            // Add share logic here
        }
    });
}
function addToCart(productId) {
    fetch(`/api/Cart/AddProductToCart/${productId}`, {
        method: 'POST'
    })
        .then(res => {
            if (res.ok) alert('Added to cart!');
            else alert('Error adding to cart.');
        })
        .catch(() => alert('Network error'));
}

