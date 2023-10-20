let carts= document.querySelectorAll('.add-cart')

let products=[
    {
        name:'Course With Forum',
        tag:'caourse1-960x640.jpg',
        price:25,
        inCart:0
    },
    {
        name:'Course With Group, News & Events',
        tag:'caourse2-960x640.jpg',
        price:20,
        inCart:0
    },
    {
        name:'Featured Course',
        tag:'caourse3-960x640.jpg',
        price:40,
        inCart:0
    },
    {
        name:'Course With Members',
        tag:'caourse5-960x640.jpg',
        price:40,
        inCart:0
    }
]

if (localStorage.getItem('productsInCart') === null) {
    localStorage.setItem('productsInCart', JSON.stringify({}));
}

for (let i = 0; i < carts.length; i++) {
    carts[i].addEventListener('click', () => {
        if (!isItemInCart(products[i])) {
            cartNumbers(products[i]);
            totalCost(products[i]);
        } else {
            alert("Item is already in the cart. You can only add once");
        }
    })
}

function isItemInCart(product) {
    let cartItems = localStorage.getItem('productsInCart');
    cartItems = JSON.parse(cartItems);

    return cartItems && cartItems[product.tag] !== undefined;
}

function onLoadCartNumbers(){
    let productNumbers=localStorage.getItem('cartNumbers')

    if (productNumbers) {
        document.querySelector('.cart span').innerText=productNumbers;
    }
}

function cartNumbers(product){
    let productNumbers= localStorage.getItem('cartNumbers')

    productNumbers=parseInt(productNumbers)

    if (productNumbers) {
        localStorage.setItem('cartNumbers', productNumbers+1)
        document.querySelector('.cart span').innerText= productNumbers + 1;
    }else{
        localStorage.setItem('cartNumbers', 1)
        document.querySelector('.cart span').innerText = 1;
    }

    setItems(product)
}

function setItems(product){
    let cartItems= localStorage.getItem('productsInCart')
    cartItems=JSON.parse(cartItems)

    if (cartItems!=null) {

        if (cartItems[product.tag] == undefined) {
            cartItems={
                ...cartItems,
                [product.tag]: product
            }
        }
        cartItems[product.tag].inCart +=1;
    }else{
        product.inCart=1;
        cartItems={
            [product.tag]: product
        }
    }

    
    localStorage.setItem('productsInCart', JSON.stringify(cartItems) )
}

function totalCost(product){
    //console.log('the product price is:', product.price);
    let cartCost= localStorage.getItem('totalCost')
    console.log('My casrtCost is:' , cartCost);
    console.log(typeof cartCost);

    if(cartCost != null){
        cartCost= parseInt(cartCost)
        localStorage.setItem('totalCost', cartCost+product.price)
    }else{
        localStorage.setItem('totalCost', product.price)
    }

    
}

function displayCart(){
    let cartItems = localStorage.getItem("productsInCart")
    let cartCost= localStorage.getItem('totalCost')
    cartItems= JSON.parse(cartItems)
    let productContainer = document.querySelector(".product-container")
    if (cartItems && productContainer) {
        productContainer.innerHTML='';
        Object.values(cartItems).map(item =>{
            let emptyCart=document.querySelector('.emptyCart')
            emptyCart.style.display='none'
            productContainer.innerHTML+=`
                <tr>
                  <th scope="row"><button class="remove-btn" data-tag="${item.tag}" style="background: none; border: none;"><i class="fa-regular fa-circle-xmark" style="color: #ee1111;"></i></button></th>
                  <td><img src="~/assets/image/${item.tag}" alt=""></td>
                  <td class="title">${item.name}</td>
                  <td class="price">$${item.price}</td>
                  <td class="quantity">${item.inCart}</td>
                  <td class="subtotal">$${item.inCart * item.price}</td>
                </tr>
            `
        })

        let subtotal= document.querySelector('.Subtotal')
        subtotal.innerHTML=cartCost

        const removeButtons = document.querySelectorAll('.remove-btn');
        removeButtons.forEach(button => {
            button.addEventListener('click', () => {
                removeItem(button.getAttribute('data-tag'));
            });
        });
    }
}

function removeItem(tag) {
    let cartItems = localStorage.getItem('productsInCart');
    cartItems = JSON.parse(cartItems);

    if (cartItems[tag]) {
        const removedItem = cartItems[tag];
        const cartCost = localStorage.getItem('totalCost');
        const updatedCost = parseInt(cartCost) - removedItem.price * removedItem.inCart;
        
        // Update cart numbers and total cost
        let productNumbers = localStorage.getItem('cartNumbers');
        productNumbers = parseInt(productNumbers);
        productNumbers -= removedItem.inCart;

        if (productNumbers <= 0) {
            localStorage.removeItem('cartNumbers');
            document.querySelector('.cart span').innerText = '';
        } else {
            localStorage.setItem('cartNumbers', productNumbers);
            document.querySelector('.cart span').innerText = productNumbers;
        }

        localStorage.setItem('totalCost', updatedCost);

        // Update the cart items
        delete cartItems[tag];
        localStorage.setItem('productsInCart', JSON.stringify(cartItems));

        // Refresh the cart display
        displayCart();
    }
}


onLoadCartNumbers();
displayCart();