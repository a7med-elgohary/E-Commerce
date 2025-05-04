document.addEventListener('DOMContentLoaded', function () {
    fetch('api/Product/Getall')
        .then(response => {
            if (!response.ok) throw new Error('Network response was not ok');
            return response.json();
        })
        .then(data => {
            // Store products globally for cart functionality
            window.all_products_json = data;

            // Get swiper containers
            const swiper_items_sale = document.getElementById("swiper_items_sale");
            const other_product_swiper = document.getElementById("other_product_swiper");
            const other_product_swiper2 = document.getElementById("other_product_swiper2");

            // Clear existing content
            [swiper_items_sale, other_product_swiper, other_product_swiper2].forEach(container => {
                if (container) container.innerHTML = '';
            });

            // Process each product
            data.forEach(product => {
                // Get product image (using first photo or default)
                const productImg = product.photos?.[0]?.url || 'default.jpg';
                const productImgHover = product.photos?.[1]?.url || productImg;

                // Create product HTML
                const productHtml = `
                    <div class="product swiper-slide" data-product-id="${product.id}">
                        <div class="icons">
                            <span><i class="fa-solid fa-cart-plus cart-icon"></i></span>
                            <span><i class="fa-solid fa-heart wishlist-icon"></i></span>
                            <span><i class="fa-solid fa-share share-icon"></i></span>
                        </div>
                        ${product.saleId ? `
                            <span class="sale_present">
                                %${Math.floor((product.old_price - product.price) / product.old_price * 100)}
                            </span>
                        ` : ''}
                        <div class="img_product">
                            <img src="${productImg}" alt="${product.name}" onerror="this.src='default.jpg'">
                            <img class="img_hover" src="${productImgHover}" alt="${product.name}" onerror="this.src='default.jpg'">
                        </div>
                        <h3 class="name_product"><a href="#">${product.name}</a></h3>
                        <div class="stars">
                            ${generateStarRating(product.rating || 5)} <!-- Default to 5 stars if no rating -->
                        </div>
                        <div class="price">
                            <p><span>$${(product.price || 0).toFixed(2)}</span></p>
                            ${product.old_price ? `<p class="old_price">$${(product.old_price || 0).toFixed(2)}</p>` : ''}
                        </div>
                    </div>
                `;

                // Add to appropriate containers
                if (product.saleId && swiper_items_sale) {
                    swiper_items_sale.innerHTML += productHtml;
                }

                if (other_product_swiper) {
                    other_product_swiper.innerHTML += productHtml;
                }

                if (other_product_swiper2) {
                    other_product_swiper2.innerHTML += productHtml;
                }
            });

            // Initialize event listeners
            setupProductEventListeners();
        })
        .catch(error => {
            console.error('Error loading products:', error);
            // Show error message to user
            const errorContainer = document.createElement('div');
            errorContainer.className = 'error-message';
            errorContainer.textContent = 'Failed to load products. Please try again later.';
            document.body.prepend(errorContainer);
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
        // Use event delegation for all product interactions
        document.addEventListener('click', function (e) {
            // Add to cart
            if (e.target.classList.contains('cart-icon')) {
                const productElement = e.target.closest('.product');
                const productId = parseInt(productElement?.dataset?.productId);
                if (productId) {
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
});

