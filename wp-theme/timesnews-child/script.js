
jQuery(document).ready(function(){

  Choise();

  
    jQuery(".filter-form button").click(function(){

      var cat = document.getElementsByName("cat")[0].value;
      var has_discount = document.getElementsByName("has_discount")[0].checked;
      var lprice = document.getElementsByName("lprice")[0].value;
      var uprice = document.getElementsByName("uprice")[0].value;

      setCookie("cat", cat, 1);
      setCookie("has_discount", has_discount, 1);
      setCookie("lprice", lprice, 1);
      setCookie("uprice", uprice, 1);
      location.reload();

    });

    jQuery(".order-now").click(function(){

      var name = document.getElementById("name").value;
      var second_name = document.getElementById("second_name").value;
      var phone = document.getElementById("phone").value;
      var address = document.getElementById("address").value;
      var process_order = "true";

      window.location = window.location+"?process_order=true;"+name+";"+second_name+";"+phone+";"+address+";";

    });

    jQuery(".add-to-bucket").click(function(){
    	let cvalue = this.getAttribute("product-id");
    	
    	let precvalue = getCookie("products");

    	setCookie("products", cvalue + "," + precvalue, 1);

      Choise();
    });

    jQuery(".remove-from-bucket").click(function(){
    	let cvalue = this.getAttribute("product-id");
    	
    	let precvalue = getCookie("products");

      let values = precvalue.split(',');

      let index = values.indexOf(cvalue);

      if(index!=-1)
      {
        if(values.length==1)
        {
          if(values[0]==cvalue)
          setCookie("products", "", 1);
        }
        else
        {
         values.splice(index,1);
         setCookie("products", values.join(","), 1);
        }
        Choise();
      }

    });
 
});

function Choise(){

  let precvalue = getCookie("products");

  let values = precvalue.split(',');

  var doms = document.getElementsByClassName("amount");

  let len = doms.length;

  for(var i = 0; i < len; i++) {
    var dom = doms[i];

    var id = dom.getAttribute("product-id");

    dom.innerText = "Ordered: "+Count(values, id);
  }

}

function Count(arr, find)
{
  var count = 0;
  for(var i = 0; i < arr.length; i++){
      if(arr[i] == find)
          count++;
  }
  return count;
}

function setCookie(cname, cvalue, exdays) {
  var d = new Date();
  d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
  var expires = "expires="+d.toUTCString();
  document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
}

function getCookie(cname) {
  var name = cname + "=";
  var ca = document.cookie.split(';');
  for(var i = 0; i < ca.length; i++) {
    var c = ca[i];
    while (c.charAt(0) == ' ') {
      c = c.substring(1);
    }
    if (c.indexOf(name) == 0) {
      return c.substring(name.length, c.length);
    }
  }
  return "";
}

function checkCookie() {
  var user = getCookie("username");
  if (user != "") {
    alert("Welcome again " + user);
  } else {
    user = prompt("Please enter your name:", "");
    if (user != "" && user != null) {
      setCookie("username", user, 365);
    }
  }
}