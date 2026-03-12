using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CookingProject.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialFinal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cuisine = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DishType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MainIngredient = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsVegetarian = table.Column<bool>(type: "bit", nullable: false),
                    IsLactoseFree = table.Column<bool>(type: "bit", nullable: false),
                    IsGlutenFree = table.Column<bool>(type: "bit", nullable: false),
                    IsSugarFree = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecipeId = table.Column<int>(type: "int", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Favorites",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecipeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favorites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Favorites_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecipeIngredients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecipeId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BasePortions = table.Column<int>(type: "int", nullable: false),
                    IsFlexible = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeIngredients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecipeIngredients_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecipeSteps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecipeId = table.Column<int>(type: "int", nullable: false),
                    StepOrder = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeSteps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecipeSteps_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Recipes",
                columns: new[] { "Id", "Cuisine", "Description", "DishType", "Image", "IsGlutenFree", "IsLactoseFree", "IsSugarFree", "IsVegetarian", "MainIngredient", "Title" },
                values: new object[,]
                {
                    { 1, "arabic", "Яйца в пряном томатном соусе с овощами.", "main", "shakshuka.jpg", true, true, true, true, "veggy", "Шакшука" },
                    { 2, "italian", "Классическая итальянская паста с мясным соусом.", "main", "pasta.jpg", false, true, false, false, "meat", "Паста болоньезе" },
                    { 3, "american", "Салат с курицей, листовым салатом и соусом Цезарь.", "salad", "salat.jpg", false, false, false, false, "meat", "Салат Цезарь" },
                    { 4, "french", "Классический луковый суп с гренками и сыром.", "soup", "soup.jpg", false, false, false, true, "veggy", "Французский луковый суп" },
                    { 5, "japan", "Роллы с рисом и лососем, подаются с соевым соусом.", "main", "rolls.jpg", true, true, true, false, "fish", "Японские роллы с лососем" },
                    { 6, "china", "Лёгкий овощной суп с тофу.", "soup", "china_soup.jpg", true, true, true, true, "veggy", "Китайский овощной суп" },
                    { 7, "russian", "Традиционный русский гарнир из гречки с грибами.", "garnish", "grechka.jpg", true, true, true, true, "grain", "Гречка с грибами" },
                    { 8, "american", "Освежающий напиток на основе лимона и мяты без добавления сахара.", "drink", "lemonade.jpg", true, true, true, true, "veggy", "Домашний лимонад без сахара" },
                    { 9, "american", "Десерт на основе ягод и орехов без глютена и лактозы.", "dessert", "berry_dessert.jpg", true, true, false, true, "veggy", "Вегетарианский ягодный десерт" },
                    { 10, "russian", "Классическая русская шарлотка с яблоками.", "dessert", "sharlotka.jpg", false, false, false, true, "grain", "Шарлотка с яблоками" }
                });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "Author", "CreatedAt", "RecipeId", "Text" },
                values: new object[,]
                {
                    { 1, "Анна", new DateTime(2026, 2, 27, 13, 23, 10, 626, DateTimeKind.Utc).AddTicks(7167), 10, "Очень вкусная шарлотка, дети в восторге!" },
                    { 2, "Игорь", new DateTime(2026, 2, 28, 13, 23, 10, 626, DateTimeKind.Utc).AddTicks(7174), 1, "Шакшука получилась острой и сытной, рекомендую." }
                });

            migrationBuilder.InsertData(
                table: "RecipeIngredients",
                columns: new[] { "Id", "Amount", "BasePortions", "IsFlexible", "Name", "RecipeId", "Unit" },
                values: new object[,]
                {
                    { 1, 4m, 4, false, "Яйца", 1, "шт." },
                    { 2, 400m, 4, false, "Помидоры", 1, "г" },
                    { 3, 1m, 4, false, "Лук", 1, "шт." },
                    { 4, 2m, 4, false, "Чеснок", 1, "зубчика" },
                    { 5, 0m, 4, true, "Паприка", 1, "по вкусу" },
                    { 6, 400m, 4, false, "Спагетти", 2, "г" },
                    { 7, 500m, 4, false, "Фарш мясной", 2, "г" },
                    { 8, 2m, 4, false, "Томатная паста", 2, "ст.л." },
                    { 9, 1m, 4, false, "Лук", 2, "шт." },
                    { 10, 400m, 4, false, "Куриная грудка", 3, "г" },
                    { 11, 200m, 4, false, "Листовой салат", 3, "г" },
                    { 12, 50m, 4, false, "Пармезан", 3, "г" },
                    { 13, 0m, 4, true, "Соус Цезарь", 3, "по вкусу" },
                    { 14, 600m, 4, false, "Лук репчатый", 4, "г" },
                    { 15, 1m, 4, false, "Бульон", 4, "л" },
                    { 16, 100m, 4, false, "Сыр грюйер", 4, "г" },
                    { 17, 8m, 4, false, "Гренки", 4, "шт." },
                    { 18, 300m, 4, false, "Рис для суши", 5, "г" },
                    { 19, 200m, 4, false, "Лосось", 5, "г" },
                    { 20, 4m, 4, false, "Нори", 5, "листа" },
                    { 21, 0m, 4, true, "Васаби", 5, "по вкусу" },
                    { 22, 200m, 4, false, "Тофу", 6, "г" },
                    { 23, 200m, 4, false, "Брокколи", 6, "г" },
                    { 24, 2m, 4, false, "Морковь", 6, "шт." },
                    { 25, 1m, 4, false, "Бульон овощной", 6, "л" },
                    { 26, 200m, 4, false, "Гречка", 7, "г" },
                    { 27, 300m, 4, false, "Шампиньоны", 7, "г" },
                    { 28, 1m, 4, false, "Лук", 7, "шт." },
                    { 29, 2m, 4, false, "Масло растительное", 7, "ст.л." },
                    { 30, 4m, 4, false, "Лимон", 8, "шт." },
                    { 31, 1m, 4, false, "Мята свежая", 8, "пучок" },
                    { 32, 1m, 4, false, "Вода", 8, "л" },
                    { 33, 0m, 4, true, "Стевия или мёд (по желанию)", 8, "по вкусу" },
                    { 34, 300m, 4, false, "Ягоды свежие", 9, "г" },
                    { 35, 100m, 4, false, "Орехи", 9, "г" },
                    { 36, 2m, 4, false, "Кокосовая стружка", 9, "ст.л." },
                    { 37, 2m, 4, false, "Мёд", 9, "ст.л." },
                    { 38, 1m, 4, false, "Мука", 10, "стакан" },
                    { 39, 1m, 4, false, "Сахар", 10, "стакан" },
                    { 40, 3m, 4, false, "Яйца", 10, "шт." },
                    { 41, 4m, 4, false, "Яблоки", 10, "шт." },
                    { 42, 0m, 4, true, "Ванилин", 10, "по вкусу" },
                    { 43, 0m, 4, true, "Сода пищевая", 10, "щепотка" }
                });

            migrationBuilder.InsertData(
                table: "RecipeSteps",
                columns: new[] { "Id", "RecipeId", "StepOrder", "Text" },
                values: new object[,]
                {
                    { 1, 1, 1, "Обжарить лук с чесноком на сковороде." },
                    { 2, 1, 2, "Добавить помидоры, специи, тушить 10 минут." },
                    { 3, 1, 3, "Сделать углубления, разбить яйца, накрыть и готовить до готовности белка." },
                    { 4, 2, 1, "Обжарить фарш с луком, добавить томатную пасту и тушить 20 мин." },
                    { 5, 2, 2, "Отварить спагетти согласно инструкции на упаковке." },
                    { 6, 2, 3, "Подавать пасту с соусом болоньезе." },
                    { 7, 3, 1, "Обжарить куриную грудку, нарезать полосками." },
                    { 8, 3, 2, "Смешать салат, курицу, пармезан, заправить соусом Цезарь." },
                    { 9, 4, 1, "Пассеровать лук до карамелизации." },
                    { 10, 4, 2, "Залить бульоном, варить 20 минут. Подавать с гренками и сыром." },
                    { 11, 5, 1, "Отварить рис для суши, остудить." },
                    { 12, 5, 2, "Раскладывать рис на нори, добавлять лосось, заворачивать роллы." },
                    { 13, 5, 3, "Нарезать роллы, подавать с соевым соусом и васаби." },
                    { 14, 6, 1, "Довести бульон до кипения, добавить нарезанные овощи и тофу." },
                    { 15, 6, 2, "Варить 10–15 минут, подавать горячим." },
                    { 16, 7, 1, "Отварить гречку." },
                    { 17, 7, 2, "Обжарить грибы с луком, смешать с гречкой." },
                    { 18, 8, 1, "Выжать сок лимонов, добавить воду и мяту." },
                    { 19, 8, 2, "Охладить, подавать со льдом." },
                    { 20, 9, 1, "Смешать ягоды, орехи, кокосовую стружку и мёд." },
                    { 21, 9, 2, "Разложить по креманкам, подавать охлаждённым." },
                    { 22, 10, 1, "Взбить яйца с сахаром, добавить муку с содой и ванильным сахаром." },
                    { 23, 10, 2, "Яблоки нарезать дольками, смешать с тестом." },
                    { 24, 10, 3, "Выложить в форму, выпекать при 190°C 30 минут." }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_RecipeId",
                table: "Comments",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_RecipeId",
                table: "Favorites",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredients_RecipeId",
                table: "RecipeIngredients",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeSteps_RecipeId",
                table: "RecipeSteps",
                column: "RecipeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Favorites");

            migrationBuilder.DropTable(
                name: "RecipeIngredients");

            migrationBuilder.DropTable(
                name: "RecipeSteps");

            migrationBuilder.DropTable(
                name: "Recipes");
        }
    }
}
