﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PruebaDataaces.Datos;
using PruebaDataaces.PruebaDataaces.Persistence.Configuration;
using PruebaDataaces.PruebaDataaces.Persistence.Configuration.dboSchema;
using System;
using System.Collections.Generic;

namespace PruebaDataaces.Datos.Configurations
{
    public partial class PersonaConfiguration : IEntityTypeConfiguration<Persona>
    {
        public void Configure(EntityTypeBuilder<Persona> entity)
        {
            entity.ToTable("Persona");

            entity.Property(e => e.Activo).HasDefaultValueSql("((1))");
            entity.Property(e => e.Cedula)
            .IsRequired()
            .HasMaxLength(13)
            .IsUnicode(false);
            entity.Property(e => e.FirstName)
            .HasMaxLength(80)
            .IsUnicode(false);
            entity.Property(e => e.LastName)
            .HasMaxLength(80)
            .IsUnicode(false);
            entity.Property(e => e.LastNameSecond)
            .HasMaxLength(80)
            .IsUnicode(false);
            entity.Property(e => e.MidleName)
            .HasMaxLength(80)
            .IsUnicode(false);
            entity.Property(e => e.RazonSocial)
            .HasMaxLength(280)
            .IsUnicode(false);
            entity.Property(e => e.Username)
            .IsRequired()
            .HasMaxLength(80)
            .IsUnicode(false);

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<Persona> entity);
    }
}
