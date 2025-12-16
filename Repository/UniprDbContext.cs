using Microsoft.EntityFrameworkCore;
using Repository.Model;

namespace Repository;

public class UniprDbContext : DbContext
{
    public UniprDbContext(DbContextOptions<UniprDbContext> options) : base(options) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    
        modelBuilder.Entity<Studente>(e => e.HasKey(s => s.Matricola));
        modelBuilder.Entity<Studente>().Property(s => s.Matricola).ValueGeneratedOnAdd();
        modelBuilder.Entity<Studente>().ToTable("studente");
        modelBuilder.Entity<Studente>()
            .Property(s => s.DataNascita)
            .HasColumnType("date");
        modelBuilder.Entity<Studente>()
            .Property(s => s.DataImmatricolazione)
            .HasColumnType("date"); 
        modelBuilder.Entity<Studente>().Property(s => s.Version)
            .IsRowVersion()
            .ValueGeneratedOnAddOrUpdate()
            .HasColumnType("bytea");

        modelBuilder.Entity<Corso>(e => e.HasKey(s => s.ID));
        modelBuilder.Entity<Corso>().Property(s => s.ID).ValueGeneratedOnAdd();
        modelBuilder.Entity<Corso>().Property(c => c.Version)
            .IsRowVersion()
            .ValueGeneratedOnAddOrUpdate()
            .HasColumnType("bytea");
        modelBuilder.Entity<Corso>().ToTable("corso");
        
        modelBuilder.Entity<Docente>(e => e.HasKey(d => d.Matricola));
        modelBuilder.Entity<Docente>().Property(d => d.Matricola).ValueGeneratedOnAdd();
        modelBuilder.Entity<Docente>().Property(d => d.Version)
            .IsRowVersion()
            .ValueGeneratedOnAddOrUpdate()
            .HasColumnType("bytea");
        modelBuilder.Entity<Docente>().ToTable("docente");
        
        modelBuilder.Entity<CorsoDocente>(b =>
        {
            b.ToTable("corso_docente");
            b.HasKey(cd => new { cd.IdCorso, cd.MatricolaDocente });

            b.HasOne(cd => cd.Corso)
                .WithMany(c => c.CorsiDocenti)
                .HasForeignKey(cd => cd.IdCorso)
                .OnDelete(DeleteBehavior.Cascade);

            b.HasOne(cd => cd.Docente)
                .WithMany(d => d.Corsi)
                .HasForeignKey(cd => cd.MatricolaDocente)
                .OnDelete(DeleteBehavior.Cascade);
            
            b.Property(cd => cd.Version)
                .IsRowVersion()
                .ValueGeneratedOnAddOrUpdate()
                .HasColumnName("rowversion")
                .HasColumnType("bytea"); 
            b.Property(s => s.DataAssegnazione)
                .HasColumnType("date"); 
        });

        modelBuilder.Entity<Esame>(e =>
        {
            e.HasKey(s => new { s.MatricolaStudente, s.IdCorso });
    
            e.ToTable("esame", t => 
            {
                t.HasCheckConstraint("CK_Voto", "(Voto >= 18 AND Voto <= 30)");
                t.HasCheckConstraint("CK_Lode", "Lode = FALSE OR Voto = 30");
            });
            
            e.HasOne(e => e.Studente)
                .WithMany(s => s.Esami)
                .HasForeignKey(e => e.MatricolaStudente);

            e.HasOne(c => c.Corso)
                .WithMany(c => c.Esami)
                .HasForeignKey(e => e.IdCorso);
            
            e.Property(s => s.DataEsame)
                .HasColumnType("date");
            
            e.Property(e => e.Version)
                .IsRowVersion()
                .ValueGeneratedOnAddOrUpdate()
                .HasColumnType("bytea");
        });
    }
    
    public DbSet<Studente> Studenti { get; set; }
    public DbSet<Corso> Corsi { get; set; }
    public DbSet<Esame> Esami { get; set; }
    public DbSet<Docente> Docenti { get; set; }
    public DbSet<CorsoDocente> CorsoDocente_ { get; set; }
}