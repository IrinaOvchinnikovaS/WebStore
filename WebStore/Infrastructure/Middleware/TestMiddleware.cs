
namespace WebStore.Infrastructure.Middleware
{
    public class TestMiddleware
    {
        private readonly RequestDelegate _Next;
        public TestMiddleware(RequestDelegate Next)
        {
            _Next = Next;
        }

        public async Task Invoke(HttpContext context)
        {
            var controller_name = context.Request.RouteValues["controller"];
            var action_name = context.Request.RouteValues["action"];

            //обработка информации из context.Request

            var processing_task = _Next(context); //далее здесь работает оставшаяся часть конвейера

            //выполнить какие-то действия параллельно асинхронно с остальной частью конвейера

            await processing_task; 

            //дообработка данных в context.Response
        }
    }
}
