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

            // Process each product for flash sale (products with old_price)
            data.forEach(product => {
                if (product.old_price) {
                    const percent_disc = Math.floor((product.old_price - product.price) / product.old_price * 100);
                    const productImg = product.photos?.[0]?.url || 'default.jpg';
                    const productImgHover = product.photos?.[1]?.url || productImg;

                    if (swiper_items_sale) {
                        swiper_items_sale.innerHTML += `
                            <div class="product swiper-slide">
                                <div class="icons">
                                    <span><i onclick="addToCart(${product.id}, this)" class="fa-solid fa-cart-plus"></i></span>
                                    <span><i class="fa-solid fa-heart"></i></span>
                                    <span><i class="fa-solid fa-share"></i></span>
                                </div>
                                <span class="sale_present">%${percent_disc}</span>
                                <a href="/Home/Prouduct/${product.id}">
                                    <div class="img_product">
                                        <img src="${productImg}" alt="${product.name}" onerror="this.src='default.jpg'">
                                        <img class="img_hover" src="${productImgHover}" alt="${product.name}" onerror="this.src='default.jpg'">
                                    </div>
                                    <h3 class="name_product"><a href="#">${product.name}</a></h3>
                                    <div class="stars">
                                        ${generateStarRating(product.rating || 5)}
                                    </div>
                                    <div class="price">
                                        <p><span>$${(product.price || 0).toFixed(2)}</span></p>
                                        <p class="old_price">$${(product.old_price || 0).toFixed(2)}</p>
                                    </div>
                                </a>
                            </div>`;
                    }
                }
            });

            // Process each product for other sections
            data.forEach(product => {
                const productImg = product.photos?.[0]?.url || 'default.jpg';
                const productImgHover = product.photos?.[1]?.url || productImg;

                const productHtml = `
                    <div class="product swiper-slide">
                        <div class="icons">
                            <span><i onclick="addToCart(${product.id}, this)" class="fa-solid fa-cart-plus"></i></span>
                            <span><i class="fa-solid fa-heart"></i></span>
                            <span><i class="fa-solid fa-share"></i></span>
                        </div>
                        <a href="/Home/Prouduct/${product.id}">
                            <div class="img_product">
                                <img src="${productImg}" alt="${product.name}" onerror="this.src='default.jpg'">
                                <img class="img_hover" src="${productImgHover}" alt="${product.name}" onerror="this.src='default.jpg'">
                            </div>
                            <h3 class="name_product"><a href="#">${product.name}</a></h3>
                            <div class="stars">
                                ${generateStarRating(product.rating || 5)}
                            </div>
                            <div class="price">
                                <p><span>$${(product.price || 0).toFixed(2)}</span></p>
                                ${product.old_price ? `<p class="old_price">$${(product.old_price || 0).toFixed(2)}</p>` : ''}
                            </div>
                        </a>
                    </div>`;

                // Add to other sections
                if (other_product_swiper) {
                    other_product_swiper.innerHTML += productHtml;
                }
                
                if (other_product_swiper2) {
                    other_product_swiper2.innerHTML += productHtml;
                }
            });

            // Set up event listeners
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

    }
});

