USE [ActiveCitizenWeb.DataAccess.FaqContext]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FaqListCategories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](1024) NULL,
	[Order] [int] NOT NULL,
 CONSTRAINT [PK_dbo.FaqListCategories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FaqListItems]    Script Date: 2016-04-16 15:27:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FaqListItems](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Question] [nvarchar](max) NULL,
	[Answer] [nvarchar](max) NULL,
	[Order] [int] NOT NULL,
	[Category_Id] [int] NULL,
 CONSTRAINT [PK_dbo.FaqListItems] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[FaqListCategories] ON 

INSERT [dbo].[FaqListCategories] ([Id], [Name], [Order]) VALUES (1, N'Как стать участником проекта «Активный гражданин»?', 1)
INSERT [dbo].[FaqListCategories] ([Id], [Name], [Order]) VALUES (2, N'Общие вопросы', 2)
INSERT [dbo].[FaqListCategories] ([Id], [Name], [Order]) VALUES (4, N'Результаты голосований', 3)
INSERT [dbo].[FaqListCategories] ([Id], [Name], [Order]) VALUES (6, N'Уникальный Идентификатор Пользователя', 4)
INSERT [dbo].[FaqListCategories] ([Id], [Name], [Order]) VALUES (7, N'Ваш пароль', 5)
SET IDENTITY_INSERT [dbo].[FaqListCategories] OFF
SET IDENTITY_INSERT [dbo].[FaqListItems] ON 

INSERT [dbo].[FaqListItems] ([Id], [Question], [Answer], [Order], [Category_Id]) VALUES (2, N'Как зарегистрироваться в «Активном гражданине»?', N'Для того чтобы зарегистрироваться с помощью сайта:<br />
                                  <span class="b-faq__lvl2">1.</span> Зайдите на сайт <a href="http://ag.mos.ru/" target="_blank">http://ag.mos.ru</a>.<br />
                                  <span class="b-faq__lvl2">2.</span> На стартовой странице нажмите кнопку «Регистрация».<br />
                                  <span class="b-faq__lvl2">3.</span> Введите свой номер мобильного телефона, отметьте пункт «Я согласен с условиями оферты» и нажмите кнопку «Зарегистрироваться». <br />
                                  <span class="b-faq__lvl2">4.</span> На ваш телефон придет SMS-сообщение с паролем для входа, который необходимо ввести в поле «Пароль», а затем нажать кнопку «Войти». <br />
                                  <span class="b-faq__lvl2">5.</span> Для того чтобы вам поступали не только общегородские, но и территориальные голосования, максимально заполните свой профиль. <br />
                                Сменить пароль можно в профиле пользователя. <br />
                                После этого вы сможете участвовать в голосованиях. <br />
                                </p>
                                <br />
                                <p>
                                Для того чтобы зарегистрироваться с помощью мобильного приложения: <br />
                                  <span class="b-faq__lvl2">1.</span> Скачайте приложение «Активный гражданин» на свой мобильный телефон (приложение поддерживает iOS, Windows Phone или Android). <br />
                                  <span class="b-faq__lvl2">2.</span> При открытии выберите пункт «Регистрация», введите свой номер мобильного телефона, отметьте пункт «Я согласен с условиями оферты» и нажмите кнопку «Зарегистрироваться». <br />
                                  <span class="b-faq__lvl2">3.</span> На ваш телефон придет SMS-сообщение с паролем для входа, который необходимо ввести в поле «Пароль», а затем нажать кнопку «Войти». <br />
                                  <span class="b-faq__lvl2">4.</span> Для того чтобы вам поступали не только общегородские, но и территориальные голосования, максимально заполните свой профиль.
                                Сменить пароль можно в разделе«Настройки». <br />
                                После этого вы сможете участвовать в голосованиях.', 1, 1)
INSERT [dbo].[FaqListItems] ([Id], [Question], [Answer], [Order], [Category_Id]) VALUES (3, N'Как можно получить баллы кроме участия в голосованиях?', N'Для того чтобы набирать максимальное количество баллов, вам необходимо придерживаться следующих правил:
                                </p>
                                <p>
                                    1. Максимально заполните ваш профиль. Укажите все возможные данные: ФИО, пол, дату рождения, семейное положение, адрес электронной почты и обязательно место жительства. Если вы укажете адрес в своем профиле, вам будут приходить не только общегородские голосования, но и территориальные, что даст вам возможность зарабатывать дополнительные баллы. Кстати, вы можете указать два адреса — регистрации и фактического места проживания, ведь голосования по каждому из этих адресов касаются именно вас. За заполненный профиль вас ждет 20 дополнительных баллов.
                                </p>
                                <p>
                                    2. Не пропускайте ни одного голосования! Как правило, голосования длятся от двух недель до месяца, так что не забывайте регулярно заходить в приложение «Активного гражданина» или на сайт ag.mos.ru. Каждое общегородское голосование принесет вам 20 баллов, а территориальное —  5 баллов.
                                </p>
                                <p>
                                    3. При регулярном посещении сайта или мобильного приложения вам также будут начисляться дополнительные баллы за активное участие — 3 балла в день.
                                </p>
                                <p>
                                    4. Приглашайте своих друзей становиться по-настоящему активными москвичами — регистрироваться в «Активном гражданине» и участвовать во всех голосованиях. За каждого приглашенного в проект друга, если он прошел регистрацию, вам начисляется 5 баллов. Пригласить друзей можно с помощью приложения «Активный гражданин»: выбранным вами контактам в вашей телефонной книге будут отправлены SMS-сообщения с приглашением к регистрации*. Приглашенные вами друзья должны зарегистрироваться в течение 30 дней с момента получения SMS с тем же номером мобильного телефона, на который пришло приглашение.Обращаем ваше внимание, что в течение месяца максимально оплачивается 10 приглашенных друзей.
                                </p>
                                <p>
                                    5. И не забывайте делиться результатами своих голосований в соцсетях! Для этого вам необходимо привязать аккаунт «Активного гражданина» к своей странице в социальной сети (Facebook, Twitter, «ВКонтакте» или «Одноклассники»), и ваши друзья будут видеть, когда вы голосуете в проекте. Вы можете как просто рассказать о проекте (оплачивается 1 пост для 1 соцсети, максимально — 4 поста для 4 соцсетей в месяц), так и поделиться фактом прохождения после каждого голосования (5 баллов за каждый пост, пока голосование активно).', 2, 1)
SET IDENTITY_INSERT [dbo].[FaqListItems] OFF
ALTER TABLE [dbo].[FaqListItems]  WITH CHECK ADD  CONSTRAINT [FK_dbo.FaqListItems_dbo.FaqListCategories_Category_Id] FOREIGN KEY([Category_Id])
REFERENCES [dbo].[FaqListCategories] ([Id])
GO
ALTER TABLE [dbo].[FaqListItems] CHECK CONSTRAINT [FK_dbo.FaqListItems_dbo.FaqListCategories_Category_Id]
GO
