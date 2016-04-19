namespace ActiveCitizenWeb.DataAccess.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using ActiveCitizen.Model.StaticContent.FAQ;
    using System.Collections.Generic;
    internal sealed class Configuration : DbMigrationsConfiguration<ActiveCitizenWeb.DataAccess.Context.FaqContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ActiveCitizenWeb.DataAccess.Context.FaqContext context)
        {
            List<FaqListCategory> categories = new List<FaqListCategory>
            {
                new FaqListCategory { Order = 1, Name = "��� ����� ���������� ������� ��������� ����������?" },
                new FaqListCategory { Order = 2, Name = "����� �������" },
                new FaqListCategory { Order = 3, Name = "���������� �����������" },
                new FaqListCategory { Order = 4, Name = "���������� ������������� ������������" },
                new FaqListCategory { Order = 5, Name = "��� ������" },
                new FaqListCategory { Order = 6, Name = "���������� �� ������������" },
                //new FaqListCategory { Order = 1, Name = "������ ��������� �����" },//this is for Moscow only
                new FaqListCategory { Order = 8, Name = "��������� � �����" },
                //new FaqListCategory { Order = 1, Name = "����������� �����" },//this is for Moscow only
                new FaqListCategory { Order = 10, Name = "�������� �� �������" },
                new FaqListCategory { Order = 11, Name = "���������� ����" }
            };

            categories.ForEach(c => context.FaqListCategory.AddOrUpdate(s => s.Order, c));
            context.SaveChanges();

            context.FaqListItem.AddOrUpdate(
                p => p.Id,
                new FaqListItem
                {
                    Id = 1,
                    Order = 1,
                    Category = categories.Single(s => s.Order == 1),
                    Question = "��� ������������������ � ��������� ����������?",
                    Answer = "<u>��� ���� ����� ������������������ � ������� �����:</u><br /> <ol> <li>������� �� ���� <a href=\"http://ag.mos.ru\">http://ag.mos.ru.</a></li> <li>�� ��������� �������� ������� ������ �������������.</li> <li>������� ���� ����� ���������� ��������, �������� ����� �� �������� � ��������� ������� � ������� ������ ��������������������.</li> <li>�� ��� ������� ������ SMS-��������� � ������� ��� �����, ������� ���������� ������ � ���� ��������, � ����� ������ ������ ������.</li> <li>��� ���� ����� ��� ��������� �� ������ �������������, �� � ��������������� �����������, ����������� ��������� ���� �������.</li> </ol> ������� ������ ����� � ������� ������������.<br />����� ����� �� ������� ����������� � ������������.<br /> <br /> <u>��� ���� ����� ������������������ � ������� ���������� ����������:</u><br /> <ol> <li>�������� ���������� ��������� ���������� �� ���� ��������� ������� (���������� ������������ iOS, Windows Phone ��� Android). <li>��� �������� �������� ����� �������������, ������� ���� ����� ���������� ��������, �������� ����� �� �������� � ��������� ������� � ������� ������ ��������������������. <li>�� ��� ������� ������ SMS-��������� � ������� ��� �����, ������� ���������� ������ � ���� ��������, � ����� ������ ������ ������. <li>��� ���� ����� ��� ��������� �� ������ �������������, �� � ��������������� �����������, ����������� ��������� ���� �������. ������� ������ ����� � ������� ����������. </ol> ����� ����� �� ������� ����������� � ������������."
                },
                new FaqListItem
                {
                    Id = 2,
                    Order = 2,
                    Category = categories.Single(s => s.Order == 1),
                    Question = "��� ����� �������� ����� ����� ������� � ������������?",
                    Answer = "��� ���� ����� �������� ������������ ���������� ������, ��� ���������� �������������� ��������� ������:<ol><li>����������� ��������� ��� �������. ������� ��� ��������� ������: ���, ���, ���� ��������, �������� ���������, ����� ����������� ����� � ����������� ����� ����������. ���� �� ������� ����� � ����� �������, ��� ����� ��������� �� ������ ������������� �����������, �� � ���������������, ��� ���� ��� ����������� ������������ �������������� �����. ������, �� ������ ������� ��� ������ � ����������� � ������������ ����� ����������, ���� ����������� �� ������� �� ���� ������� �������� ������ ���. �� ����������� ������� ��� ���� 20 �������������� ������.</li><li>�� ����������� �� ������ �����������! ��� �������, ����������� ������ �� ���� ������ �� ������, ��� ��� �� ��������� ��������� �������� � ���������� ���������� ���������� ��� �� ���� ag.mos.ru. ������ ������������� ����������� �������� ��� 20 ������, � ��������������� � 5 ������.</li><li>��� ���������� ��������� ����� ��� ���������� ���������� ��� ����� ����� ����������� �������������� ����� �� �������� ������� � 3 ����� � ����.</li><li>����������� ����� ������ ����������� ��-���������� ��������� ���������� � ���������������� � ��������� ���������� � ����������� �� ���� ������������. �� ������� ������������� � ������ �����, ���� �� ������ �����������, ��� ����������� 5 ������. ���������� ������ ����� � ������� ���������� ��������� ����������: ��������� ���� ��������� � ����� ���������� ����� ����� ���������� SMS-��������� � ������������ � �����������*. ������������ ���� ������ ������ ������������������ � ������� 30 ���� � ������� ��������� SMS � ��� �� ������� ���������� ��������, �� ������� ������ �����������.�������� ���� ��������, ��� � ������� ������ ����������� ������������ 10 ������������ ������.</li><li>� �� ��������� �������� ������������ ����� ����������� � ��������! ��� ����� ��� ���������� ��������� ������� ���������� ���������� � ����� �������� � ���������� ���� (Facebook, Twitter, ���������� ��� ��������������), � ���� ������ ����� ������, ����� �� ��������� � �������. �� ������ ��� ������ ���������� � ������� (������������ 1 ���� ��� 1 �������, ����������� � 4 ����� ��� 4 �������� � �����), ��� � ���������� ������ ����������� ����� ������� ����������� (5 ������ �� ������ ����, ���� ����������� �������).</li></ol>* - �������� SMS �������������� � ������������ � ��������� ������ ��������� �����."
                },
                new FaqListItem
                {
                    Id = 3,
                    Order = 3,
                    Category = categories.Single(s => s.Order == 1),
                    Question = "��� ����� � ���� ������� � ������� ����������?",
                    Answer = "����� ����� � ���� ������� � ������� ����������, ��� ���������� ������ ���� ������ (����� � ������) � ��������� ���������� ���������� ���������� ��� �� ����� ag.mos.ru. ����������, ��� ����� ������� �������� ����� ������ ���������� ��������, ������� � �������� (��������, 999 1234567)."
                },
                new FaqListItem
                {
                    Id = 4,
                    Order = 1,
                    Category = categories.Single(s => s.Order == 2),
                    Question = "����� ����������� �������� ������������?",
                    Answer = "��� ����������� � ������� ������������ ����� ����������� ��������� ���� � �������, ������� ���, �������, ����� ���������� ��� ������. ���������� ���� ����� ������� �� �������� ������������ ��� ���������� �������. ������ � ��� � ����������� �� ����� ����������� �� ������� ���������� ��������� ������ ������������� �������������. ���� ������� �������� ����������, �� ������� ������� ����� ������ � ������������ �� ������������� ��������. �������� � ������� ������ ���������� �������� ��������� ������� � ������������ �� �������� ������ � ������. ��������� ���� ������ �� �������� �����������, ��������� ����������� ����� ������ ������������, ��������� � ������� ������� ����� ���������������� ��������. ��� ������� � ������� ������� � ������� ��������, ������� ����� ������� ����������� ����������, ��������, ������� � ��������� ��������, ���������� ������������� ������������ ������ �������� ������ � ����� ��� ������ ������� �� ������� ��������������� � ������������� ����� ������ ������. ����� � ������ �������� �� ���� ������� ������������ ���������� ������� ���, ���������� ������, �������� � ��������, �����. ��� ������� ������ ���������� ���, ������� ��������� ��� ���������� �������� ������� � ����������� �����. ��������� ������� ��������� ���������� ����������� � ����������� ���������� �����������, ���������� �� �������������."
                },
                new FaqListItem
                {
                    Id = 5,
                    Order = 2,
                    Category = categories.Single(s => s.Order == 2),
                    Question = "��� ���������� ����� �����������?",
                    Answer = "��� ���� ����� ������� ������� � �����������, ��� ������� ����� � ������ ������������� � ������ �� ��������� �������. ���� �� ����������� ��������� ����������� - ��� ��������� ����� �������������. ����� ����, ��� ��������� ����������� ������������ �� ������ ��������� ����������, ������� ������������� ����������� ��� ����� � ����������.<br />����������� ������� �� ���������� ������� � ��������� �����������, ����� ���� ������ �� ����� ������."
                },
                new FaqListItem
                {
                    Id = 6,
                    Order = 3,
                    Category = categories.Single(s => s.Order == 2),
                    Question = "��� �������� ������ � �������� ������������?",
                    Answer = "��� ������� � ��������������� ������������ ��� ���������� ������� � ����� ������� ����� ����������� �/��� ����� ������������ ����������. ��������������� ����������� �������� ������������ �������� ���� �������. �������� ������ � ������������ �� �������, � ������� �� �� ����������/�� ����������������, ����������."
                });
            context.SaveChanges();
        }
    }
}
