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
                <div class="product swiper-slide">
                    <div class="icons">
                        <span><i onclick="addToCart(${product.id}, this)" class="fa-solid fa-cart-plus"></i></span>
                        <span><i class="fa-solid fa-heart"></i></span>
                        <span><i class="fa-solid fa-share"></i></span>
                    </div>

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
            `;
        });
    })
    .catch(error => {
        console.error('Error fetching products:', error);
    });