var image = document.getElementsByClassName('thumbnail');
new simpleParallax(image, {
	scale: 1.5
});



const first = document.getElementById("icon-first")

first.addEventListener("click", function() {
    document.getElementById("first").style.display="flex"
    document.getElementById("second").style.display="none"
    document.getElementById("third").style.display="none"

    document.getElementById('icon-color-1').style.backgroundColor="#ffcc21"
    document.getElementById('icon-color-2').style.backgroundColor="#fff"
    document.getElementById('icon-color-3').style.backgroundColor="#fff"
})

const second = document.getElementById("icon-second")
second.addEventListener("click", function() {
    document.getElementById("first").style.display="none"
    document.getElementById("second").style.display="flex"
    document.getElementById("third").style.display="none"

    document.getElementById('icon-color-1').style.backgroundColor="#fff"
    document.getElementById('icon-color-2').style.backgroundColor="#ffcc21"
    document.getElementById('icon-color-3').style.backgroundColor="#fff"
})

const third = document.getElementById("icon-third")
third.addEventListener("click", function() {
    document.getElementById("first").style.display="none"
    document.getElementById("second").style.display="none"
    document.getElementById("third").style.display="flex"

    document.getElementById('icon-color-1').style.backgroundColor="#fff"
    document.getElementById('icon-color-2').style.backgroundColor="#fff"
    document.getElementById('icon-color-3').style.backgroundColor="#ffcc21"
})

