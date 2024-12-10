//using Microsoft.AspNetCore.Mvc;
//using RZA_OMwebsite.Components.Pages;
//using Stripe.Forwarding;
//using Stripe.Events;
//using Stripe.Checkout;
//using Stripe;

//[Route("api/stripehooks")]
//public class StripeWebHook : Controller
//{
//    private readonly IConfiguration _configuration;

//    public StripeWebHook(IConfiguration configuration)
//    {
//        _configuration = configuration;
//    }

//    [HttpPost]
//    public async Task<IActionResult> Index()
//    {
//        var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
//        var secret = _configuration.GetValue<string>("StripeWebHookSecret");

//        try
//        {
//            var stripeEvent = EventUtility.ConstructEvent(
//                json,
//                Request.Headers["Stripe-Signature"],
//                secret
//            );

//            // Handle the checkout.session.completed event
//            if (stripeEvent.Type == Events.CheckoutSessionCompleted)
//            {
//                var session = stripeEvent.Data.Object as Session;
//                var options = new SessionGetOptions();
//                options.AddExpand("line_items");

//                var service = new SessionService();
//                // Retrieve the session. If you require line items in the response, you may include them by expanding line_items.
//                var sessionWithLineItems = await service.GetAsync(session.Id, options);
//                StripeList<LineItem> lineItems = sessionWithLineItems.LineItems;

//                // do something here based on the order's line items!
//            }

//            return Ok();
//        }
//        catch (StripeException e)
//        {
//            return BadRequest();
//        }
//    }
//}