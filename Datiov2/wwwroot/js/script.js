

var deleteButton = document.querySelectorAll('#delete');

deleteButton.forEach(function (button) {
  button.addEventListener('click', function () {
    var parentElement = button.parentElement.parentElement.parentElement;
    parentElement.remove();
    calculateSum();
  });
});



// function that calculates the total price of the cart items and displays it

// if the page is kundvagn.html or favoriter.html 
if (window.location.href.includes("kundvagn.html") || window.location.href.includes("favoriter.html")) {
  calculateSum();
}

function calculateSum() {
  let total = 0;
  const prices = document.querySelectorAll('.cart_item_price');

  prices.forEach(price => {
    total += Number(price.textContent.replace(/[^\d.]/g, ''));

  });
  document.querySelector('.cart_sum').textContent = `Totalt: ${total} kr`;
}



$(function () {
  $("#sortable_area").sortable({
    axis: "y",
    containment: "parent",
    cursor: "move",
    opacity: 0.75,
    tolerance: "pointer",
    scroll: true,
    placeholder: "ui-state-highlight"
  });
  $("#sortable_area").disableSelection();
});


function goToCartNudge() {
  var cart_icon = document.getElementById("shopping_cart_icon");
  var cart_button = document.getElementById("shopping_cart_button");

  cart_button.addEventListener("mouseover", function () {
    if (cart_icon.innerHTML == "shopping_cart") {
      cart_icon.innerHTML = "shopping_cart_checkout";
      cart_button.classList.toggle("cart_nudge");
    }
  });

  cart_button.addEventListener("mouseout", function () {
    if (cart_icon.innerHTML == "shopping_cart_checkout") {
      cart_icon.innerHTML = "shopping_cart";
      cart_button.classList.toggle("cart_nudge");
    }
  }
  );
} 

var slideIndex = 1;
showSlides(slideIndex);

setInterval(function () {
  nextSlide();
}, 6000);

function nextSlide() {
  showSlides((slideIndex += 1));
}

function prevSlide() {
  showSlides((slideIndex -= 1));
}

function showSlides(n) {
  var slides = document.getElementsByClassName("slide");

  if (n > slides.length) {
    slideIndex = 1;
  }
  if (n < 1) {
    slideIndex = slides.length;
  }
  for (let i = 0; i < slides.length; i++) {
    slides[i].style.opacity = 0.8;
    slides[i].style.display = "none";
  }

  slides[slideIndex - 1].style.display = "block";
  $(slides[slideIndex - 1]).fadeTo(1000, 0.95);
}


function meny_funktion() {
  var menu = $(".meny_alternativ");
  var menu_category = $(".mobile_category");
  var icon = $("#mobile_menu_icon");

  if (menu.is(":hidden")) {
    menu.css("display", "none").slideDown(220);
    menu_category.css("display", "flex").slideDown(200);

    icon.animate({
      "opacity": 0.4,
      "font-size": "48px"
    }, 200, function () {
      icon.html("close").animate({
        "opacity": 1,
        "font-size": "64px"
      }, 200);
    });

  } else {
    function menu_slideup() {
      menu_category.slideUp(400);
      setTimeout(function () {
        menu.slideUp(100);
      }, 300);
    }
    menu_slideup();

    icon.animate({
      "opacity": 0.4,
      "font-size": "48px"
    }, 200, function () {
      icon.html("menu").animate({
        "opacity": 1,
        "font-size": "64px"
      }, 200);
    });
  }
}









function darkmode() {

  var darkmode = document.querySelectorAll(".header, .form_search, .content-container, .produkt_beställning_box, .produkt_sida_upper, .produkt_beskrivning_box, .produkt_specifikationer, .produkt_sida_down");
  var darkmode_dark = document.querySelectorAll(".main-container, .produkt_box, body");
  var darkmode_light = document.querySelectorAll(".produkt_box, .next_products,  .sök_knapp, .search, .konto, .kundvagn, .product_category_box");

  var darkmode_light_color = document.querySelectorAll(".product_showcase_type_text, .produkt_beskrivning_text, .produkt_specifikationer, .produkt_beställning_box");
  var darkmode_dark_color = document.querySelectorAll(".produkt_specifikationer");

  for (var i = 0; i < darkmode.length; i++) {
    darkmode[i].classList.toggle("dark-mode");
  }

  for (var i = 0; i < darkmode_dark.length; i++) {
    darkmode_dark[i].classList.toggle("dark-mode-dark");
  }

  for (var i = 0; i < darkmode_light.length; i++) {
    darkmode_light[i].classList.toggle("dark-mode-light");
  }

  for (var i = 0; i < darkmode_light_color.length; i++) {
    darkmode_light_color[i].classList.toggle("dark-mode-light-color");
  }

  for (var i = 0; i < darkmode_dark_color.length; i++) {
    darkmode_dark_color[i].classList.toggle("dark-mode-dark-color");
  }

}


function category_view_large() {
  const category_view_button = document.querySelector(".product_category_field");
  category_view_button.style.gridTemplateColumns = "auto auto auto auto auto auto";
}

function category_view_small() {
  const category_view_button = document.querySelector(".product_category_field");
  category_view_button.style.gridTemplateColumns = "auto auto auto auto";
}





