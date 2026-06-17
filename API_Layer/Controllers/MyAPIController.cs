using Business_Layer;
using Business_Layer.Models;
using Data_Accesst_Layer;
using Graduation_Project.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Claims;
using System.Text;
using static Business_Layer.UserDetail;
using static Data_Access_Layer.FavoriteData;




namespace Graduation_Projecte.Controllers
{
    [Route("api/Graduation_Project")]
    [ApiController]
    public class MyAPIController : ControllerBase
    {


        private readonly IFawaterakPaymentService _fawaterakService;
        private readonly IConfiguration _configuration;

        public MyAPIController(IFawaterakPaymentService fawaterakService, IConfiguration configuration)
        {
            _fawaterakService = fawaterakService;
            _configuration = configuration;
        }
        


      


        [HttpPost("SignUp")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Register([FromBody] SignUpRequest request)
        {
            if (string.IsNullOrEmpty(request.FristName) || string.IsNullOrEmpty(request.LastName) || string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest(new { message = "Please enter the rest of your information!" });
            }

            var newUser = Users.SignUp(
                request.FristName,
                request.LastName,
                request.Email,
                request.Password

            );

            if (newUser == null)
            {
                return BadRequest(new { status = "Error", message = "Email already exists!" });
            }

            return Ok(new
            {
                status = "Success",
                message = "Account created successfully",
                userId = newUser.ID_User
            });
        }

        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UserLogin([FromBody] Login_Request request)
        {
            var user = Users.Login(request);

            if (user != null)
            {
                // Create JWT Token
                var authClaims = new[]
                {
            new Claim(ClaimTypes.Name,user.FirstName + " " + user.LastName),
            new Claim(ClaimTypes.NameIdentifier, user.ID_User.ToString()),
            new Claim("Project", "Graduation2026")
        };

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    expires: DateTime.Now.AddDays(7), // The token is valid for a week
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
          
                return Ok(new
                {
                    status = "Success",
                    message = "Welcome " + user.FirstName + " " + user.LastName,
                    userId = user.ID_User,
                    token = new JwtSecurityTokenHandler().WriteToken(token) 
                });
            }

            return Unauthorized(new { status = "Error", message = "Invalid Email or Password" });
        }





        [Authorize]
        [HttpGet("All_Products", Name = "GetAllProduct")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Product>> GetAllStudents()
        {


            List<Product> ProductList = Products.GetAllProducts();
            if (ProductList.Count == 0)
            {
                return NotFound("No Products Found!");
            }
            foreach (var product in ProductList)
            {
                if (!string.IsNullOrEmpty(product.Image))
                {
                    //show image with Details
                    product.Image = "https://shila-unanatomised-mouthily.ngrok-free.dev/api/Graduation_Project/GetImage/" + product.Image;
                }

            }
            return Ok(ProductList);

        }

        [Authorize]
        [HttpGet("Beauty_Products", Name = "GetBeautyProducts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Product>> GetBeautyProducts()
        {


            List<Product> ProductList = Products.GetBeautyProducts();
            if (ProductList.Count == 0)
            {
                return NotFound("No Products Found!");
            }
            foreach (var product in ProductList)
            {
                if (!string.IsNullOrEmpty(product.Image))
                {
                    product.Image = "https://shila-unanatomised-mouthily.ngrok-free.dev/api/Graduation_Project/GetImage/" + product.Image;
                }

            }
            return Ok(ProductList);

        }

        [Authorize]
        [HttpGet("Fragrances_Products", Name = "GetFragrancesProducts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Product>> GetFragrancesProducts()
        {


            List<Product> ProductList = Products.GetFragrancesProducts();
            if (ProductList.Count == 0)
            {
                return NotFound("No Products Found!");
            }
            foreach (var product in ProductList)
            {
                if (!string.IsNullOrEmpty(product.Image))
                {
                    product.Image = "https://shila-unanatomised-mouthily.ngrok-free.dev/api/Graduation_Project/GetImage/" + product.Image;
                }

            }
            return Ok(ProductList);

        }

        [Authorize]
        [HttpGet("Furniture_Products", Name = "GetFurnitureProducts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Product>> GetFurnitureProducts()
        {


            List<Product> ProductList = Products.GetFurnitureProducts();
            if (ProductList.Count == 0)
            {
                return NotFound("No Products Found!");
            }
            foreach (var product in ProductList)
            {
                if (!string.IsNullOrEmpty(product.Image))
                {
                    product.Image = "https://shila-unanatomised-mouthily.ngrok-free.dev/api/Graduation_Project/GetImage/" + product.Image;
                }

            }
            return Ok(ProductList);

        }

        [Authorize]
        [HttpGet("Groeries_Products", Name = "GetGroeriesProducts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Product>> GetGroeriesProducts()
        {


            List<Product> ProductList = Products.GetGroeriesProducts();
            if (ProductList.Count == 0)
            {
                return NotFound("No Products Found!");
            }
            foreach (var product in ProductList)
            {
                if (!string.IsNullOrEmpty(product.Image))
                {
                    product.Image = "https://shila-unanatomised-mouthily.ngrok-free.dev/api/Graduation_Project/GetImage/" + product.Image;
                }

            }
            return Ok(ProductList);

        }

        [Authorize]
        [HttpGet("HomeDecoration_Products", Name = "GetHomeDecorationProducts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Product>> GetHomeDecorationProducts()
        {


            List<Product> ProductList = Products.GetHomeDecorationProducts();
            if (ProductList.Count == 0)
            {
                return NotFound("No Products Found!");
            }
            foreach (var product in ProductList)
            {
                if (!string.IsNullOrEmpty(product.Image))
                {
                    product.Image = "https://shila-unanatomised-mouthily.ngrok-free.dev/api/Graduation_Project/GetImage/" + product.Image;
                }

            }
            return Ok(ProductList);

        }

        [Authorize]
        [HttpGet("Jewelery_Products", Name = "GetJeweleryProducts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Product>> GetJeweleryProducts()
        {


            List<Product> ProductList = Products.GetJeweleryProducts();
            if (ProductList.Count == 0)
            {
                return NotFound("No Products Found!");
            }
            foreach (var product in ProductList)
            {
                if (!string.IsNullOrEmpty(product.Image))
                {
                    product.Image = "https://shila-unanatomised-mouthily.ngrok-free.dev/api/Graduation_Project/GetImage/" + product.Image;
                }

            }
            return Ok(ProductList);

        }
        [Authorize]
        [HttpGet("MenIsClothing_Products", Name = "GetMenIsClothingProducts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Product>> GetMenIsClothingProducts()
        {


            List<Product> ProductList = Products.GetMenIsClothingProducts();
            if (ProductList.Count == 0)
            {
                return NotFound("No Products Found!");
            }
            foreach (var product in ProductList)
            {
                if (!string.IsNullOrEmpty(product.Image))
                {
                    product.Image = "https://shila-unanatomised-mouthily.ngrok-free.dev/api/Graduation_Project/GetImage/" + product.Image;
                }

            }
            return Ok(ProductList);

        }

        [Authorize]
        [HttpGet("SwomenIsClothing_Products", Name = "GetSwomenIsClothingProducts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Product>> GetSwomenIsClothingProducts()
        {


            List<Product> ProductList = Products.GetSwomenIsClothingProducts();
            if (ProductList.Count == 0)
            {
                return NotFound("No Products Found!");
            }
            foreach (var product in ProductList)
            {
                if (!string.IsNullOrEmpty(product.Image))
                {
                    product.Image = "https://shila-unanatomised-mouthily.ngrok-free.dev/api/Graduation_Project/GetImage/" + product.Image;
                }

            }
            return Ok(ProductList);

        }


        // Endpoint to handle image upload
        [Authorize]
        [HttpPost("Upload")]
        public async Task<IActionResult> UploadImage(IFormFile imageFile)
        {
            // Check if no file is uploaded
            if (imageFile == null || imageFile.Length == 0)
                return BadRequest("No file uploaded.");

            // Directory where files will be uploaded
            var uploadDirectory = @"F:\MyUploadImageOfGradstion_Profect";

            // Generate a unique filename
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
            var filePath = Path.Combine(uploadDirectory, fileName);

            // Ensure the uploads directory exists, create if it doesn't
            if (!Directory.Exists(uploadDirectory))
            {
                Directory.CreateDirectory(uploadDirectory);
            }

            // Save the file to the server
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            // Return the file path as a response
            return Ok(new { filePath });
        }

        [HttpGet("GetImage/{fileName}")]
        public IActionResult GetImage(string fileName)
        {
            // Directory where files are stored
            var uploadDirectory = @"F:\MyUploadImageOfGradstion_Profect";
            var filePath = Path.Combine(uploadDirectory, fileName);

            // Check if the file exists
            if (!System.IO.File.Exists(filePath))
                return NotFound("Image not found.");

            // Open the image file for reading
            var image = System.IO.File.OpenRead(filePath);
            var mimeType = GetMimeType(filePath);

            // Return the file with the correct MIME type
            return File(image, mimeType);
        }
        private string GetMimeType(string filePath)
        {
            var extension = Path.GetExtension(filePath).ToLowerInvariant();
            return extension switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                _ => "application/octet-stream",
            };
        }

        [Authorize]
        [HttpGet("Search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Search([FromQuery] string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return BadRequest("Please enter the product name");

            var results = Products.GetSearchQuery(name);
            return results.Any() ? Ok(results) : NotFound("No results");
        }



        [Authorize]
        [HttpPost("AddToCart")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AddToCart([FromBody] Cart request)
        {
          
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized("User ID not found in token");

            int userId = int.Parse(userIdClaim);

            try
            {
           
                string result = Carts.AddToCart(userId, request.ProductID, request.Quantity);

                if (result == "success")
                    return Ok(new { status = true, message = "Added to the cart successfully" });

                return BadRequest(new { status = false, message = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = false, message = "Server error", error = ex.Message });
            }
        }


        [Authorize]
        [HttpGet("GetCartItems")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetCart() 
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdClaim)) return Unauthorized();
            int userId = int.Parse(userIdClaim);

            var items = Carts.GetCustomerCart(userId);
            decimal grandTotal = items.Sum(x => x.TotalPrice);

            return Ok(new
            {
                status = true,
                cartItems = items,
                finalGrandTotal = grandTotal
            });
        }


        [Authorize]
        [HttpDelete("RemoveFromCart")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult DeleteFromCart(int productId)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdClaim)) return Unauthorized();

            int userId = int.Parse(userIdClaim);

            bool isDeleted = Carts.DeleteFromCart(userId, productId);

            if (isDeleted)
            {
                return Ok(new { message = "The product has been deleted successfully" });
            }
            else
            {
                return BadRequest(new { message = "the product Not delete " });
            }

        }




        [Authorize]
        [HttpPut("UpdateQuantity")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateQuantity(int productId, int newQuantity)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int userId = int.Parse(userIdClaim);

            if (Carts.UpdateQuantity(userId, newQuantity, productId))
            {
                return Ok(new { message = "The quantity has been updated" });
            }
            else
            {
                return BadRequest(new { message = "Update failed" });
            }
        }
        [Authorize]
        [HttpDelete("ClearCart")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult ClearAll()
        {

            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int userId = int.Parse(userIdClaim);

            Carts.ClearCustomerCart(userId); 
            return Ok(new { message = "The basket has been completely emptied" });
        }


        [Authorize]
        [HttpPost("AddToFavorite")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AddToFavorite([FromBody] FavoriteRequest request)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized(new { message = "You must log in first" });
            }

            int userId = int.Parse(userIdClaim.Value);

            if (Favorites.AddToFavorites(userId, request.productId))
            {
                return Ok(new { message = "Added to favorites successfully" });
            }
            else
            {
                return BadRequest(new { message = "The product is already in the favorites or an error occurred" });
            }
        }

        [Authorize]
        [HttpDelete("RemoveFromFavorite")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult RemoveFromFavorite([FromBody] FavoriteRequest request)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            int userId = int.Parse(userIdClaim.Value);

            if (Favorites.RemoveFromFavorites(userId, request.productId))
            {
                return Ok(new { message = "The product has been removed from the favorites" });
            }

            return BadRequest(new { message = "An error occurred while deleting" });
        }
        [Authorize]
        [HttpGet("GetAllFavorites")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllFavorites()
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                return Unauthorized(new { message = "Please log in firstاً" });
            }

            int userId = int.Parse(userIdClaim.Value);

            DataTable dt = Favorites.GetAllFavorites(userId);

            var favoritesList = new List<object>();
            foreach (DataRow row in dt.Rows)
            {
                favoritesList.Add(new
                {
                    productId = row["ID_Product"],
                    name = row["Name"],
                    price = row["Price"],
                    description = row["Size"],
                    imagePath = row["Image"]
                });
            }

            return Ok(favoritesList);
        }





        [Authorize]
        [HttpPut("UpdateMyAddress&Phone")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        
        public IActionResult UpdateMyContact([FromBody] UpdateContactDto contactDto)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null) return Unauthorized("The token is invalid or expired.");

            int userId = int.Parse(userIdClaim.Value);

            if (Users.UpdateContactInfo(userId, contactDto.Address, contactDto.Phone))
            {
                return Ok(new { status = "success", message = "Your data has been updated successfully." });
            }

            return BadRequest("Failed to update data, please try again.");
        }




        [Authorize]
        [HttpPost("Checkout")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Checkout()
        {
            try
            {
                var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
                if (userIdClaim == null) return Unauthorized(new { message = "User not found in token" });

                int userId = int.Parse(userIdClaim.Value);

                string result = Orders.ProcessCheckout(userId);

                if (int.TryParse(result, out int orderId))
                {
                    return Ok(new
                    {
                        success = true,
                        orderId = orderId,
                        message = "Order created successfully from your cart!"
                    });
                }
                else
                {
                    //The basket is empty
                    return BadRequest(new { success = false, message = result });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", details = ex.Message });
            }
        }



        // POST: api/MyAPI/checkout
        [Authorize]
        [HttpPost("CreateInvoice")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateInvoice([FromBody] EInvoiceRequestModel request)
        {
            
            var data = await _fawaterakService.CreateEInvoiceAsync(request);

            if (data is null)
            {
                return BadRequest("Failed to create the invoice, please check the submitted data");
            }

            return Ok(data);
        }



        [Authorize]
        [HttpPost("PayNow")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> PayNow([FromBody] PaymentRequestDto request)
        {
            // استخراج JWT Token 
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
            {
                return Unauthorized(new { message = "Unknown user" });
            }

            int userId = int.Parse(userIdClaim);

            var paymentUrl = await _fawaterakService.ProcessOrderPayment(request.OrderId, userId);

            if (string.IsNullOrEmpty(paymentUrl))
            {
                return BadRequest(new { message = "Failed to prepare the payment link, please check the order number" });
            }

            return Ok(new { url = paymentUrl });
        }

        [HttpPost("Callbackt")]
        public async Task<IActionResult> WebhookPaid([FromBody] FawaterakWebhookModel model)
        {
            if (model == null) return BadRequest();


            // Data processing and database updating
            bool result = await _fawaterakService.WebhookAsync(model);

            if (result)
            {
                return Ok(new { message = "Order Processed Successfully" });
            }

            return BadRequest("Processing Failed");
        }



        [Authorize]
        [HttpGet("MyOrders")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
       
        public IActionResult GetMyOrdersHistory()
        {
            try
            {
                var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);

                if (userIdClaim == null)
                {
                    return Unauthorized("User data cannot be found in the token.");
                }

                int userId = int.Parse(userIdClaim.Value);

              
                var orders = Orders.GetUserOrdersDetailed(userId);

                if (orders == null || !orders.Any())
                {
                    return NotFound("There are no previous requests for this user.");
                }

                return Ok(orders);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"A technical error occurred: {ex.Message}");
            }
        }

        [Authorize]
        [HttpGet("MyDetails")]
        public IActionResult GetDetails()
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);

            if (userIdClaim == null) return Unauthorized("No entry allowed.");

            int userId = int.Parse(userIdClaim.Value);

            var details = clsUser.GetMyDetails(userId);

            if (details == null) return NotFound("User data does not exist.");

            return Ok(details);
        }

        [Authorize]
        [HttpPut("UpdateMyDetails")]
        public IActionResult UpdateDetails([FromBody] UserProfileDTO updateDto)
        {
            var userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value);

            if (clsUser.UpdateMyDetails(userId, updateDto))
            {
                return Ok(new { message = "Your profile has been updated successfully." });
            }

            return BadRequest("Data update failed, please try again later.");
        }



        [Authorize]
        [HttpPost("Logout")]
        public IActionResult Logout()
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (LogoutS.AddTokenToBlacklist(token))
            {
                return Ok(new { message = "You have been logged out successfully" });
            }
            return BadRequest(new { message = "Logout failed" });
        }
    }

}

















