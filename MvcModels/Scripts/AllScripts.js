var map;
var geocoder;
var markers = [];


//////////////////////////////////////////////////////////////////////////////////////////////
//                          Google Maps
// ////////////////////////////////////////////////////////////////////////////////////////////
function initializeMap()
{
    var mapOptions =
    {
        center: new google.maps.LatLng(31.501857, 35.141051),
        zoom: 7,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };

    map         = new google.maps.Map(document.getElementById("map"), mapOptions);
    geocoder    = new google.maps.Geocoder();
}

function deleteAllMarkers()
{
    for (var marker in markers)
    {
        marker.setMap(null);
    }

    markers = [];
}

function addMarker(address, title, content)
{
    geocoder.geocode({ 'address': address }, function (results, status)
    {
        if (status == google.maps.GeocoderStatus.OK)
        {
            var myLatLng =
            {
                lat: results[0].geometry.location.lat(),
                lng: results[0].geometry.location.lng()
            };

            var marker = new google.maps.Marker({
                position: myLatLng,
                map: map,
                animation: google.maps.Animation.DROP,
                title: title
            });

            var infHtml = "<div margine-top='5px'><h1>" + title + "</h1></div>" +
                "<div>" + content + "</div>"
            var infowindow = new google.maps.InfoWindow(
            {
                content: infHtml
            })

            marker.addListener('click', function ()
            {
                infowindow.open(map, marker);
            });

            markers.push(marker);
        }
    });
}

google.maps.event.addDomListener(window, 'load', initializeMap);

//////////////////////////////////////////////////////////////////////////////////////////////
//                          Ajax
//////////////////////////////////////////////////////////////////////////////////////////////
$(function ()
{
    $('a.link').on("click",function ()
    {
        $.ajax(
        {
            url: $(this).attr('href'),
            type: "GET",
            success: function (response)
            {
                if (response.redirect) {
                    window.location = response.url;
                } else {
                    $('#MainContent').html(response);
                }
            }
        });
        return false;
    });
});

//$(function () {
//    $('input').on("click", function () {
//        $.ajax(
//        {
//            url: $(this).attr('href'),
//            type: "POST",
//            success: function (response) {
//                $('#MainContent').html(response);
//            }
//        });
//        return false;
//    });
//});

//////////////////////////////////////////////////////////////////////////////////////////////
//                          Facebook
//////////////////////////////////////////////////////////////////////////////////////////////
var pac;
var uac;
window.fbAsyncInit = function ()
{
    FB.init(
    {
        appId: '100797163614698',
        xfbml: true,
        version: 'v2.5'
    });

    FB.login(function (response)
    {
        // Logged into your app and Facebook.
        if (response.status === 'connected')
        {
            FB.api("/me/accounts", function (response)
            {
                if (response && !response.error)
                {
                    pac = response.data[0].access_token;
                }
            });

            uac = response.authResponse.accessToken;
        }
        // The person is logged into Facebook, but not your app.
        else if (response.status === 'not_authorized')
        {
        }
        // The person is not logged into Facebook, so we're not sure if
        // they are logged into this app or not.
        else
        {   
        }
    }, { scope: 'publish_pages, manage_pages, publish_actions' });
};

(function(d, s, id) 
{
    var js, fjs = d.getElementsByTagName(s)[0];
    if (d.getElementById(id)) return;
    js = d.createElement(s); js.id = id;
    js.src = "//connect.facebook.net/en_US/sdk.js#xfbml=1&version=v2.5&appId=100797163614698";
    fjs.parentNode.insertBefore(js, fjs);
}
(document, 'script', 'facebook-jssdk'));

var fbLogout = function()
{
    FB.logout(function (response)
    {
        // Person is now logged out
    });
}

var fbPagePost = function ()
{
    debugger
    FB.api('/v2.5/500529936789725/feed', "POST", { access_token: pac, message: $("#postText").val() }, function (response)
    {
      console.log(JSON.stringify(response));
    });
}


