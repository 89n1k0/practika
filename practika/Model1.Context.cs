﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace practika
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class praktikaEntities : DbContext
    {
        public praktikaEntities()
            : base("name=praktikaEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Заказ> Заказ { get; set; }
        public virtual DbSet<Заказанные_изделия> Заказанные_изделия { get; set; }
        public virtual DbSet<Изделия> Изделия { get; set; }
        public virtual DbSet<Пользователь> Пользователь { get; set; }
        public virtual DbSet<Рисунки> Рисунки { get; set; }
        public virtual DbSet<Роли> Роли { get; set; }
        public virtual DbSet<Составы> Составы { get; set; }
        public virtual DbSet<Типы> Типы { get; set; }
        public virtual DbSet<Ткани> Ткани { get; set; }
        public virtual DbSet<Фурнитура> Фурнитура { get; set; }
        public virtual DbSet<Цвета> Цвета { get; set; }
    }
}