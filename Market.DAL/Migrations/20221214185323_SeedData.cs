using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Market.DAL.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Carts",
                columns: new[] { "Id", "UserId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Фрукты" },
                    { 2, "Овощи" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "admin" },
                    { 2, "user" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "ImgPath", "Name", "Price" },
                values: new object[,]
                {
                    { 1, 1, "Яблоко сорта «Карамелька» такое же сладкое, как и его название! Плоды с преобладающим красным румянцем порадуют нежной рассыпчатой мякотью и насыщенным медовым вкусом. «Карамелька» может служить прекрасным перекусом, а также самостоятельным десертом. Кроме того, из таких яблок получаются отличные компоты и варенья.", "/img/apples3.jpg", "Яблоко Карамелька", 50m },
                    { 2, 1, "Банан настолько популярный и любимый многими фрукт, что не нуждается в представлении! Яркий жёлтый цвет, нежная сладкая мякоть и насыщенный аромат делают его безусловным фаворитом среди взрослых и детей. Свежий банан очень питательный, поэтому помогает быстро утолить голод. Съесть его можно на ходу, не запачкав руки, ведь банан очень удобно чистить. Этот фрукт широко применяются в кулинарии. Без них не было бы детского питания, нежных панкейков, творожков, йогуртов и мюсли.", "/img/bannana.jpg", "Банан", 70m },
                    { 3, 2, "Этот овощ популярен и любим во всём мире! Жареные золотистые дольки, ароматное пюре из детства, цельные клубни, сваренные «в мундире» – картофель прекрасен в любых кулинарных проявлениях. Вкус этого продукта столь же уникален, как и он сам. В приготовленном виде он, как правило, имеет мягкую крахмалистую текстуру и сладковатый привкус. Какая бы кулинарная идея с участием картофеля ни пришла вам в голову, воплотить её будет нетрудно!", "/img/potatos.jpg", "Картофель, 400г", 44m },
                    { 4, 2, "Гладкие огурцы, как правило, длиннее обычных и покрыты гладкой глянцевой кожицей. Этот сорт идеально подходит для приготовления различных закусок. Они не горчат, обладают отличным вкусом и выразительным ароматом. Благодаря плотной и в то же время очень нежной мякоти, из них получаются превосходные овощные роллы. Гладкие огурцы удобно нарезать тонкими пластинками, которые легко сворачиваются в рулетики. В качестве начинки попробуйте тунец, лосось или творог с чесноком и зеленью.", "/img/cucmbers.jpg", "Огурцы, 200г", 30m },
                    { 5, 1, "Мандарины любят многие, ведь они дарят ощущение ностальгии по детству. Без их вкуса и аромата просто не существует новогоднего стола и праздничного настроения! Эти маленькие цитрусы имеют сочную мякоть и искристый кисло-сладкий вкус. Разделить такой фрукт с кем-то приятным – что может быть лучше! А еще с мандарином можно смело экспериментировать в кулинарии. Попробуйте приготовить лёгкий салат, добавив к мандарину камамбер и руколу.", "/img/mandarin.jpg", "Мандарины, 100г", 70m },
                    { 6, 1, "Легендарное египетское манго снова в городе!\r\nСладко-сочное, безволокнистое, с нежной бархатистой текстурой. Обманчиво зелёное снаружи, но идеально спелое внутри. Манго уже можно купить в наших магазинах или заказать на дом с бесплатной доставкой.\r\nВес одного манго — от 500 грамм. ", "/img/mango.jpg", "Манго, 1 шт", 90m }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CartId", "Name", "Password", "RoleId" },
                values: new object[,]
                {
                    { 1, null, "admin", "3DA24438DA894C18C863FFD9C9D2F1D6C98FE1633BEA6B56FE4EB1B95A927579", 1 },
                    { 2, null, "user123", "CBB9B63D553D88A72F0D8AFE8B14800A8511028FE03CD5D37D18B15B9E2A7BB8", 2 }
                });

            migrationBuilder.InsertData(
                table: "CartItems",
                columns: new[] { "Id", "CartId", "Count", "ProductId" },
                values: new object[,]
                {
                    { 1, 2, 1, 5 },
                    { 2, 1, 1, 4 },
                    { 3, 1, 2, 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CartItems",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CartItems",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "CartItems",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
