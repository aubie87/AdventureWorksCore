console.log('Hello from AdWorks!');

var theForm = document.getElementById('theForm');
theForm.hidden = true;

var theForm2 = $('#theForm');
theForm2.show();

var button = $('#buyButton');
button.on('click', function () {
    console.log('Buy an item');
});

var productInfo = $('.person-properties li');
productInfo.on('click', function () {
    console.log('You clicked on ' + $(this).text());
});

