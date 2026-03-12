-- Применение миграции AddRecipeIngredientsAndSteps
-- Выполните этот скрипт в базе CookingProjectDb (SQL Server Management Studio или sqlcmd)

USE CookingProjectDb;
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'RecipeIngredients')
BEGIN
    CREATE TABLE [RecipeIngredients] (
        [Id] int NOT NULL IDENTITY(1,1),
        [RecipeId] int NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        [Amount] decimal(18,2) NOT NULL,
        [Unit] nvarchar(max) NOT NULL,
        [BasePortions] int NOT NULL,
        [IsFlexible] bit NOT NULL,
        CONSTRAINT [PK_RecipeIngredients] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_RecipeIngredients_Recipes_RecipeId] FOREIGN KEY ([RecipeId]) REFERENCES [Recipes] ([Id]) ON DELETE CASCADE
    );
    CREATE INDEX [IX_RecipeIngredients_RecipeId] ON [RecipeIngredients] ([RecipeId]);
END
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'RecipeSteps')
BEGIN
    CREATE TABLE [RecipeSteps] (
        [Id] int NOT NULL IDENTITY(1,1),
        [RecipeId] int NOT NULL,
        [StepOrder] int NOT NULL,
        [Text] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_RecipeSteps] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_RecipeSteps_Recipes_RecipeId] FOREIGN KEY ([RecipeId]) REFERENCES [Recipes] ([Id]) ON DELETE CASCADE
    );
    CREATE INDEX [IX_RecipeSteps_RecipeId] ON [RecipeSteps] ([RecipeId]);
END
GO

-- Заполнение данными (если таблицы пустые)
IF NOT EXISTS (SELECT 1 FROM [RecipeIngredients])
BEGIN
    SET IDENTITY_INSERT [RecipeIngredients] ON;
    INSERT INTO [RecipeIngredients] (Id, RecipeId, Name, Amount, Unit, BasePortions, IsFlexible) VALUES
    (1,1,N'Яйца',4,N'шт.',4,0),(2,1,N'Помидоры',400,N'г',4,0),(3,1,N'Лук',1,N'шт.',4,0),(4,1,N'Чеснок',2,N'зубчика',4,0),(5,1,N'Паприка',0,N'по вкусу',4,1),
    (6,2,N'Спагетти',400,N'г',4,0),(7,2,N'Фарш мясной',500,N'г',4,0),(8,2,N'Томатная паста',2,N'ст.л.',4,0),(9,2,N'Лук',1,N'шт.',4,0),
    (10,3,N'Куриная грудка',400,N'г',4,0),(11,3,N'Листовой салат',200,N'г',4,0),(12,3,N'Пармезан',50,N'г',4,0),(13,3,N'Соус Цезарь',0,N'по вкусу',4,1),
    (14,4,N'Лук репчатый',600,N'г',4,0),(15,4,N'Бульон',1,N'л',4,0),(16,4,N'Сыр грюйер',100,N'г',4,0),(17,4,N'Гренки',8,N'шт.',4,0),
    (18,5,N'Рис для суши',300,N'г',4,0),(19,5,N'Лосось',200,N'г',4,0),(20,5,N'Нори',4,N'листа',4,0),(21,5,N'Васаби',0,N'по вкусу',4,1),
    (22,6,N'Тофу',200,N'г',4,0),(23,6,N'Брокколи',200,N'г',4,0),(24,6,N'Морковь',2,N'шт.',4,0),(25,6,N'Бульон овощной',1,N'л',4,0),
    (26,7,N'Гречка',200,N'г',4,0),(27,7,N'Шампиньоны',300,N'г',4,0),(28,7,N'Лук',1,N'шт.',4,0),(29,7,N'Масло растительное',2,N'ст.л.',4,0),
    (30,8,N'Лимон',4,N'шт.',4,0),(31,8,N'Мята свежая',1,N'пучок',4,0),(32,8,N'Вода',1,N'л',4,0),(33,8,N'Стевия или мёд (по желанию)',0,N'по вкусу',4,1),
    (34,9,N'Ягоды свежие',300,N'г',4,0),(35,9,N'Орехи',100,N'г',4,0),(36,9,N'Кокосовая стружка',2,N'ст.л.',4,0),(37,9,N'Мёд',2,N'ст.л.',4,0),
    (38,10,N'Мука',1,N'стакан',4,0),(39,10,N'Сахар',1,N'стакан',4,0),(40,10,N'Яйца',3,N'шт.',4,0),(41,10,N'Яблоки',4,N'шт.',4,0),(42,10,N'Ванилин',0,N'по вкусу',4,1),(43,10,N'Сода пищевая',0,N'щепотка',4,1);
    SET IDENTITY_INSERT [RecipeIngredients] OFF;
END
GO

IF NOT EXISTS (SELECT 1 FROM [RecipeSteps])
BEGIN
    SET IDENTITY_INSERT [RecipeSteps] ON;
    INSERT INTO [RecipeSteps] (Id, RecipeId, StepOrder, Text) VALUES
    (1,1,1,N'Обжарить лук с чесноком на сковороде.'),(2,1,2,N'Добавить помидоры, специи, тушить 10 минут.'),(3,1,3,N'Сделать углубления, разбить яйца, накрыть и готовить до готовности белка.'),
    (4,2,1,N'Обжарить фарш с луком, добавить томатную пасту и тушить 20 мин.'),(5,2,2,N'Отварить спагетти согласно инструкции на упаковке.'),(6,2,3,N'Подавать пасту с соусом болоньезе.'),
    (7,3,1,N'Обжарить куриную грудку, нарезать полосками.'),(8,3,2,N'Смешать салат, курицу, пармезан, заправить соусом Цезарь.'),
    (9,4,1,N'Пассеровать лук до карамелизации.'),(10,4,2,N'Залить бульоном, варить 20 минут. Подавать с гренками и сыром.'),
    (11,5,1,N'Отварить рис для суши, остудить.'),(12,5,2,N'Раскладывать рис на нори, добавлять лосось, заворачивать роллы.'),(13,5,3,N'Нарезать роллы, подавать с соевым соусом и васаби.'),
    (14,6,1,N'Довести бульон до кипения, добавить нарезанные овощи и тофу.'),(15,6,2,N'Варить 10–15 минут, подавать горячим.'),
    (16,7,1,N'Отварить гречку.'),(17,7,2,N'Обжарить грибы с луком, смешать с гречкой.'),
    (18,8,1,N'Выжать сок лимонов, добавить воду и мяту.'),(19,8,2,N'Охладить, подавать со льдом.'),
    (20,9,1,N'Смешать ягоды, орехи, кокосовую стружку и мёд.'),(21,9,2,N'Разложить по креманкам, подавать охлаждённым.'),
    (22,10,1,N'Взбить яйца с сахаром, добавить муку с содой и ванильным сахаром.'),(23,10,2,N'Яблоки нарезать дольками, смешать с тестом.'),(24,10,3,N'Выложить в форму, выпекать при 190°C 30 минут.');
    SET IDENTITY_INSERT [RecipeSteps] OFF;
END
GO

-- Регистрация миграции в EF Core (чтобы dotnet ef не пытался применить её повторно)
IF NOT EXISTS (SELECT 1 FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260224080000_AddRecipeIngredientsAndSteps')
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20260224080000_AddRecipeIngredientsAndSteps', N'7.0.20');
GO

PRINT 'Миграция AddRecipeIngredientsAndSteps успешно применена.';
