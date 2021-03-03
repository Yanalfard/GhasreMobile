
//.........................██████╗░░░░░░██╗░█████╗░██╗░░██╗
//.........................██╔══██╗░░░░░██║██╔══██╗╚██╗██╔╝
//.........................██████╦╝░░░░░██║███████║░╚███╔╝░
//.........................██╔══██╗██╗░░██║██╔══██║░██╔██╗░
//.........................██████╦╝╚█████╔╝██║░░██║██╔╝╚██╗
//.........................╚═════╝░░╚════╝░╚═╝░░╚═╝╚═╝░░╚═╝

//TODO: Import Jquery to your project;;
var returner;
const ApiUrl = '';
function Bjax(url, input, methodType)
{
    if (methodType === 'SP') //Short POST
        $.ajax(
            {
                url: ApiUrl + url + input,
                method: 'Post',
                contentType: 'application/json',
                dataType: 'json',
                async: false,
                success: function (data)
                {
                    returner = data;
                },
                error: function ()
                {
                    returner = false;
                }
            });
    else if (methodType === 'LP') //Long POST
        $.ajax(
            {
                url: ApiUrl + url,
                data: JSON.stringify(input),
                method: 'Post',
                contentType: 'application/json',
                dataType: 'json',
                async: false,
                success: function (data)
                {
                    returner = data;
                },
                error: function ()
                {
                    returner = false;
                }
            });
    else if (methodType === 'G')//GET
        $.ajax(
            {
                url: ApiUrl + url,
                method: 'Get',
                contentType: 'application/json',
                dataType: 'json',
                async: false,
                success: function (data)
                {
                    returner = data;
                },
                error: function ()
                {
                    returner = false;
                }
            });
    return returner;
}