using CookingProject.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace CookingProject.Api.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Recipe> Recipes => Set<Recipe>();
    public DbSet<RecipeIngredient> RecipeIngredients => Set<RecipeIngredient>();
    public DbSet<RecipeStep> RecipeSteps => Set<RecipeStep>();
    public DbSet<Comment> Comments => Set<Comment>();
    public DbSet<Favorite> Favorites => Set<Favorite>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Recipe>().HasData(
            // 1. Шакшука — arabic, veggy, veg, nolact, nogluten, nosugar
            new Recipe
            {
                Id = 1,
                Title = "Шакшука",
                Description = "Яйца в пряном томатном соусе с овощами.",
                Image = "shakshuka.jpg",
                Cuisine = "arabic",
                DishType = "main",
                MainIngredient = "veggy",
                IsVegetarian = true,
                IsLactoseFree = true,
                IsGlutenFree = true,
                IsSugarFree = true
            },
            // 2. Паста болоньезе — italian, meat, main
            new Recipe
            {
                Id = 2,
                Title = "Паста болоньезе",
                Description = "Классическая итальянская паста с мясным соусом.",
                Image = "pasta.jpg",
                Cuisine = "italian",
                DishType = "main",
                MainIngredient = "meat",
                IsVegetarian = false,
                IsLactoseFree = true,
                IsGlutenFree = false,
                IsSugarFree = false
            },
            // 3. Салат Цезарь — american, salad, meat
            new Recipe
            {
                Id = 3,
                Title = "Салат Цезарь",
                Description = "Салат с курицей, листовым салатом и соусом Цезарь.",
                Image = "salat.jpg",
                Cuisine = "american",
                DishType = "salad",
                MainIngredient = "meat",
                IsVegetarian = false,
                IsLactoseFree = false,
                IsGlutenFree = false,
                IsSugarFree = false
            },
            // 4. Французский луковый суп — french, soup, veggy
            new Recipe
            {
                Id = 4,
                Title = "Французский луковый суп",
                Description = "Классический луковый суп с гренками и сыром.",
                Image = "soup.jpg",
                Cuisine = "french",
                DishType = "soup",
                MainIngredient = "veggy",
                IsVegetarian = true,
                IsLactoseFree = false,
                IsGlutenFree = false,
                IsSugarFree = false
            },
            // 5. Японские роллы с лососем — japan, main, fish, nogluten
            new Recipe
            {
                Id = 5,
                Title = "Японские роллы с лососем",
                Description = "Роллы с рисом и лососем, подаются с соевым соусом.",
                Image = "rolls.jpg",
                Cuisine = "japan",
                DishType = "main",
                MainIngredient = "fish",
                IsVegetarian = false,
                IsLactoseFree = true,
                IsGlutenFree = true,
                IsSugarFree = true
            },
            // 6. Китайский овощной суп — china, soup, veggy, veg, nolact, nogluten, nosugar
            new Recipe
            {
                Id = 6,
                Title = "Китайский овощной суп",
                Description = "Лёгкий овощной суп с тофу.",
                Image = "china_soup.jpg",
                Cuisine = "china",
                DishType = "soup",
                MainIngredient = "veggy",
                IsVegetarian = true,
                IsLactoseFree = true,
                IsGlutenFree = true,
                IsSugarFree = true
            },
            // 7. Русская гречка с грибами — russian, garnish, grain, veg, nolact, nogluten
            new Recipe
            {
                Id = 7,
                Title = "Гречка с грибами",
                Description = "Традиционный русский гарнир из гречки с грибами.",
                Image = "grechka.jpg",
                Cuisine = "russian",
                DishType = "garnish",
                MainIngredient = "grain",
                IsVegetarian = true,
                IsLactoseFree = true,
                IsGlutenFree = true,
                IsSugarFree = true
            },
            // 8. Домашний лимонад без сахара — drink, veggy, nolact, nogluten, nosugar
            new Recipe
            {
                Id = 8,
                Title = "Домашний лимонад без сахара",
                Description = "Освежающий напиток на основе лимона и мяты без добавления сахара.",
                Image = "lemonade.jpg",
                Cuisine = "american",
                DishType = "drink",
                MainIngredient = "veggy",
                IsVegetarian = true,
                IsLactoseFree = true,
                IsGlutenFree = true,
                IsSugarFree = true
            },
            // 9. Вегетарианский десерт с ягодами — dessert, veg, nolact, nogluten
            new Recipe
            {
                Id = 9,
                Title = "Вегетарианский ягодный десерт",
                Description = "Десерт на основе ягод и орехов без глютена и лактозы.",
                Image = "berry_dessert.jpg",
                Cuisine = "american",
                DishType = "dessert",
                MainIngredient = "veggy",
                IsVegetarian = true,
                IsLactoseFree = true,
                IsGlutenFree = true,
                IsSugarFree = false
            },
            // 10. Русская шарлотка с яблоками — russian, dessert, grain
            new Recipe
            {
                Id = 10,
                Title = "Шарлотка с яблоками",
                Description = "Классическая русская шарлотка с яблоками.",
                Image = "sharlotka.jpg",
                Cuisine = "russian",
                DishType = "dessert",
                MainIngredient = "grain",
                IsVegetarian = true,
                IsLactoseFree = false,
                IsGlutenFree = false,
                IsSugarFree = false
            }
        );

        modelBuilder.Entity<Comment>().HasData(
            new Comment
            {
                Id = 1,
                RecipeId = 10,
                Author = "Анна",
                Text = "Очень вкусная шарлотка, дети в восторге!",
                CreatedAt = DateTime.UtcNow.AddDays(-2)
            },
            new Comment
            {
                Id = 2,
                RecipeId = 1,
                Author = "Игорь",
                Text = "Шакшука получилась острой и сытной, рекомендую.",
                CreatedAt = DateTime.UtcNow.AddDays(-1)
            }
        );

        // Ингредиенты и шаги для пересчёта порций
        modelBuilder.Entity<RecipeIngredient>().HasData(
            // Шакшука (1), 4 порции
            new RecipeIngredient { Id = 1, RecipeId = 1, Name = "Яйца", Amount = 4, Unit = "шт.", BasePortions = 4, IsFlexible = false },
            new RecipeIngredient { Id = 2, RecipeId = 1, Name = "Помидоры", Amount = 400, Unit = "г", BasePortions = 4, IsFlexible = false },
            new RecipeIngredient { Id = 3, RecipeId = 1, Name = "Лук", Amount = 1, Unit = "шт.", BasePortions = 4, IsFlexible = false },
            new RecipeIngredient { Id = 4, RecipeId = 1, Name = "Чеснок", Amount = 2, Unit = "зубчика", BasePortions = 4, IsFlexible = false },
            new RecipeIngredient { Id = 5, RecipeId = 1, Name = "Паприка", Amount = 0, Unit = "по вкусу", BasePortions = 4, IsFlexible = true },
            // Паста болоньезе (2), 4 порции
            new RecipeIngredient { Id = 6, RecipeId = 2, Name = "Спагетти", Amount = 400, Unit = "г", BasePortions = 4, IsFlexible = false },
            new RecipeIngredient { Id = 7, RecipeId = 2, Name = "Фарш мясной", Amount = 500, Unit = "г", BasePortions = 4, IsFlexible = false },
            new RecipeIngredient { Id = 8, RecipeId = 2, Name = "Томатная паста", Amount = 2, Unit = "ст.л.", BasePortions = 4, IsFlexible = false },
            new RecipeIngredient { Id = 9, RecipeId = 2, Name = "Лук", Amount = 1, Unit = "шт.", BasePortions = 4, IsFlexible = false },
            // Салат Цезарь (3), 4 порции
            new RecipeIngredient { Id = 10, RecipeId = 3, Name = "Куриная грудка", Amount = 400, Unit = "г", BasePortions = 4, IsFlexible = false },
            new RecipeIngredient { Id = 11, RecipeId = 3, Name = "Листовой салат", Amount = 200, Unit = "г", BasePortions = 4, IsFlexible = false },
            new RecipeIngredient { Id = 12, RecipeId = 3, Name = "Пармезан", Amount = 50, Unit = "г", BasePortions = 4, IsFlexible = false },
            new RecipeIngredient { Id = 13, RecipeId = 3, Name = "Соус Цезарь", Amount = 0, Unit = "по вкусу", BasePortions = 4, IsFlexible = true },
            // Французский луковый суп (4), 4 порции
            new RecipeIngredient { Id = 14, RecipeId = 4, Name = "Лук репчатый", Amount = 600, Unit = "г", BasePortions = 4, IsFlexible = false },
            new RecipeIngredient { Id = 15, RecipeId = 4, Name = "Бульон", Amount = 1, Unit = "л", BasePortions = 4, IsFlexible = false },
            new RecipeIngredient { Id = 16, RecipeId = 4, Name = "Сыр грюйер", Amount = 100, Unit = "г", BasePortions = 4, IsFlexible = false },
            new RecipeIngredient { Id = 17, RecipeId = 4, Name = "Гренки", Amount = 8, Unit = "шт.", BasePortions = 4, IsFlexible = false },
            // Японские роллы (5), 4 порции
            new RecipeIngredient { Id = 18, RecipeId = 5, Name = "Рис для суши", Amount = 300, Unit = "г", BasePortions = 4, IsFlexible = false },
            new RecipeIngredient { Id = 19, RecipeId = 5, Name = "Лосось", Amount = 200, Unit = "г", BasePortions = 4, IsFlexible = false },
            new RecipeIngredient { Id = 20, RecipeId = 5, Name = "Нори", Amount = 4, Unit = "листа", BasePortions = 4, IsFlexible = false },
            new RecipeIngredient { Id = 21, RecipeId = 5, Name = "Васаби", Amount = 0, Unit = "по вкусу", BasePortions = 4, IsFlexible = true },
            // Китайский овощной суп (6), 4 порции
            new RecipeIngredient { Id = 22, RecipeId = 6, Name = "Тофу", Amount = 200, Unit = "г", BasePortions = 4, IsFlexible = false },
            new RecipeIngredient { Id = 23, RecipeId = 6, Name = "Брокколи", Amount = 200, Unit = "г", BasePortions = 4, IsFlexible = false },
            new RecipeIngredient { Id = 24, RecipeId = 6, Name = "Морковь", Amount = 2, Unit = "шт.", BasePortions = 4, IsFlexible = false },
            new RecipeIngredient { Id = 25, RecipeId = 6, Name = "Бульон овощной", Amount = 1, Unit = "л", BasePortions = 4, IsFlexible = false },
            // Гречка с грибами (7), 4 порции
            new RecipeIngredient { Id = 26, RecipeId = 7, Name = "Гречка", Amount = 200, Unit = "г", BasePortions = 4, IsFlexible = false },
            new RecipeIngredient { Id = 27, RecipeId = 7, Name = "Шампиньоны", Amount = 300, Unit = "г", BasePortions = 4, IsFlexible = false },
            new RecipeIngredient { Id = 28, RecipeId = 7, Name = "Лук", Amount = 1, Unit = "шт.", BasePortions = 4, IsFlexible = false },
            new RecipeIngredient { Id = 29, RecipeId = 7, Name = "Масло растительное", Amount = 2, Unit = "ст.л.", BasePortions = 4, IsFlexible = false },
            // Лимонад (8), 4 порции
            new RecipeIngredient { Id = 30, RecipeId = 8, Name = "Лимон", Amount = 4, Unit = "шт.", BasePortions = 4, IsFlexible = false },
            new RecipeIngredient { Id = 31, RecipeId = 8, Name = "Мята свежая", Amount = 1, Unit = "пучок", BasePortions = 4, IsFlexible = false },
            new RecipeIngredient { Id = 32, RecipeId = 8, Name = "Вода", Amount = 1, Unit = "л", BasePortions = 4, IsFlexible = false },
            new RecipeIngredient { Id = 33, RecipeId = 8, Name = "Стевия или мёд (по желанию)", Amount = 0, Unit = "по вкусу", BasePortions = 4, IsFlexible = true },
            // Ягодный десерт (9), 4 порции
            new RecipeIngredient { Id = 34, RecipeId = 9, Name = "Ягоды свежие", Amount = 300, Unit = "г", BasePortions = 4, IsFlexible = false },
            new RecipeIngredient { Id = 35, RecipeId = 9, Name = "Орехи", Amount = 100, Unit = "г", BasePortions = 4, IsFlexible = false },
            new RecipeIngredient { Id = 36, RecipeId = 9, Name = "Кокосовая стружка", Amount = 2, Unit = "ст.л.", BasePortions = 4, IsFlexible = false },
            new RecipeIngredient { Id = 37, RecipeId = 9, Name = "Мёд", Amount = 2, Unit = "ст.л.", BasePortions = 4, IsFlexible = false },
            // Шарлотка (10), 4 порции
            new RecipeIngredient { Id = 38, RecipeId = 10, Name = "Мука", Amount = 1, Unit = "стакан", BasePortions = 4, IsFlexible = false },
            new RecipeIngredient { Id = 39, RecipeId = 10, Name = "Сахар", Amount = 1, Unit = "стакан", BasePortions = 4, IsFlexible = false },
            new RecipeIngredient { Id = 40, RecipeId = 10, Name = "Яйца", Amount = 3, Unit = "шт.", BasePortions = 4, IsFlexible = false },
            new RecipeIngredient { Id = 41, RecipeId = 10, Name = "Яблоки", Amount = 4, Unit = "шт.", BasePortions = 4, IsFlexible = false },
            new RecipeIngredient { Id = 42, RecipeId = 10, Name = "Ванилин", Amount = 0, Unit = "по вкусу", BasePortions = 4, IsFlexible = true },
            new RecipeIngredient { Id = 43, RecipeId = 10, Name = "Сода пищевая", Amount = 0, Unit = "щепотка", BasePortions = 4, IsFlexible = true }
        );

        modelBuilder.Entity<RecipeStep>().HasData(
            // Шакшука
            new RecipeStep { Id = 1, RecipeId = 1, StepOrder = 1, Text = "Обжарить лук с чесноком на сковороде." },
            new RecipeStep { Id = 2, RecipeId = 1, StepOrder = 2, Text = "Добавить помидоры, специи, тушить 10 минут." },
            new RecipeStep { Id = 3, RecipeId = 1, StepOrder = 3, Text = "Сделать углубления, разбить яйца, накрыть и готовить до готовности белка." },
            // Паста болоньезе
            new RecipeStep { Id = 4, RecipeId = 2, StepOrder = 1, Text = "Обжарить фарш с луком, добавить томатную пасту и тушить 20 мин." },
            new RecipeStep { Id = 5, RecipeId = 2, StepOrder = 2, Text = "Отварить спагетти согласно инструкции на упаковке." },
            new RecipeStep { Id = 6, RecipeId = 2, StepOrder = 3, Text = "Подавать пасту с соусом болоньезе." },
            // Салат Цезарь
            new RecipeStep { Id = 7, RecipeId = 3, StepOrder = 1, Text = "Обжарить куриную грудку, нарезать полосками." },
            new RecipeStep { Id = 8, RecipeId = 3, StepOrder = 2, Text = "Смешать салат, курицу, пармезан, заправить соусом Цезарь." },
            // Луковый суп
            new RecipeStep { Id = 9, RecipeId = 4, StepOrder = 1, Text = "Пассеровать лук до карамелизации." },
            new RecipeStep { Id = 10, RecipeId = 4, StepOrder = 2, Text = "Залить бульоном, варить 20 минут. Подавать с гренками и сыром." },
            // Роллы
            new RecipeStep { Id = 11, RecipeId = 5, StepOrder = 1, Text = "Отварить рис для суши, остудить." },
            new RecipeStep { Id = 12, RecipeId = 5, StepOrder = 2, Text = "Раскладывать рис на нори, добавлять лосось, заворачивать роллы." },
            new RecipeStep { Id = 13, RecipeId = 5, StepOrder = 3, Text = "Нарезать роллы, подавать с соевым соусом и васаби." },
            // Китайский суп
            new RecipeStep { Id = 14, RecipeId = 6, StepOrder = 1, Text = "Довести бульон до кипения, добавить нарезанные овощи и тофу." },
            new RecipeStep { Id = 15, RecipeId = 6, StepOrder = 2, Text = "Варить 10–15 минут, подавать горячим." },
            // Гречка с грибами
            new RecipeStep { Id = 16, RecipeId = 7, StepOrder = 1, Text = "Отварить гречку." },
            new RecipeStep { Id = 17, RecipeId = 7, StepOrder = 2, Text = "Обжарить грибы с луком, смешать с гречкой." },
            // Лимонад
            new RecipeStep { Id = 18, RecipeId = 8, StepOrder = 1, Text = "Выжать сок лимонов, добавить воду и мяту." },
            new RecipeStep { Id = 19, RecipeId = 8, StepOrder = 2, Text = "Охладить, подавать со льдом." },
            // Ягодный десерт
            new RecipeStep { Id = 20, RecipeId = 9, StepOrder = 1, Text = "Смешать ягоды, орехи, кокосовую стружку и мёд." },
            new RecipeStep { Id = 21, RecipeId = 9, StepOrder = 2, Text = "Разложить по креманкам, подавать охлаждённым." },
            // Шарлотка
            new RecipeStep { Id = 22, RecipeId = 10, StepOrder = 1, Text = "Взбить яйца с сахаром, добавить муку с содой и ванильным сахаром." },
            new RecipeStep { Id = 23, RecipeId = 10, StepOrder = 2, Text = "Яблоки нарезать дольками, смешать с тестом." },
            new RecipeStep { Id = 24, RecipeId = 10, StepOrder = 3, Text = "Выложить в форму, выпекать при 190°C 30 минут." }
        );
    }
}

