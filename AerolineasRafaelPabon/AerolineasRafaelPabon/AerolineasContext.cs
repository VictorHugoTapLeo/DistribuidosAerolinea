using AerolineasRafaelPabon.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.ComponentModel.DataAnnotations.Schema;

namespace AerolineasRafaelPabon
{
    public class AerolineasContext : DbContext
    {
        // Constructor sin parámetros que usa la cadena de conexión del App.config
        public AerolineasContext() : base("name=AerolineasConnection")
        {
        }

        // Definición de DbSets (tablas)
        public DbSet<Destino> Destinos { get; set; }
        public DbSet<Ruta> Rutas { get; set; }
        public DbSet<TipoNave> TiposNaves { get; set; }
        public DbSet<Nave> Naves { get; set; }
        public DbSet<Vuelo> Vuelos { get; set; }
        public DbSet<Pasajero> Pasajeros { get; set; }
        public DbSet<Asiento> Asientos { get; set; }
        public DbSet<Transaccion> Transacciones { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Configuraciones de modelo (fluent API)

            // Configurar claves únicas
            modelBuilder.Entity<Destino>()
        .HasIndex(d => d.CodigoDestino)
        .IsUnique();

            // Configuración de Ruta
            modelBuilder.Entity<Ruta>()
                .HasRequired(r => r.Origen)
                .WithMany(d => d.RutasOrigen)
                .HasForeignKey(r => r.OrigenID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Ruta>()
                .HasRequired(r => r.Destino)
                .WithMany(d => d.RutasDestino)
                .HasForeignKey(r => r.DestinoID)
                .WillCascadeOnDelete(false);

            // Configuración para decimales
            modelBuilder.Entity<TipoNave>()
                .Property(t => t.CostoTurista)
                .HasPrecision(10, 2);

            modelBuilder.Entity<TipoNave>()
                .Property(t => t.CostoEjecutivo)
                .HasPrecision(10, 2);


            modelBuilder.Entity<Nave>()
                .HasIndex(n => n.CodigoNave)
                .IsUnique();

            // Configuración de Vuelo
            modelBuilder.Entity<Vuelo>()
                .HasRequired(v => v.Ruta)
                .WithMany(r => r.Vuelos)
                .HasForeignKey(v => v.RutaID);

            modelBuilder.Entity<Vuelo>()
                .HasRequired(v => v.Nave)
                .WithMany(n => n.Vuelos)
                .HasForeignKey(v => v.NaveID);
            modelBuilder.Entity<Vuelo>()
        .HasMany(v => v.Asientos)
        .WithRequired(a => a.Vuelo)
        .HasForeignKey(a => a.VueloID);
            modelBuilder.Entity<Pasajero>()
                .HasIndex(p => p.Pasaporte)
                .IsUnique();

            modelBuilder.Entity<Asiento>()
                .HasIndex(a => a.CodigoAsiento)
                .IsUnique();

            modelBuilder.Entity<Transaccion>()
        .Property(t => t.Costo)
        .HasPrecision(10, 2);

            base.OnModelCreating(modelBuilder);
        }
    }
}
