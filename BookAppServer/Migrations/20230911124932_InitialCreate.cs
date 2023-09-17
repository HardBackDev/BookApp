using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookAppServer.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    AuthorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Bio = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.AuthorId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    Genres = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuthorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.BookId);
                    table.ForeignKey(
                        name: "FK_Books_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "AuthorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserBooks",
                columns: table => new
                {
                    UserBookId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBooks", x => x.UserBookId);
                    table.ForeignKey(
                        name: "FK_UserBooks_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserBooks_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "76c97f52-fcce-41e3-acfa-7e6b5264eaf9", null, "Admin", "ADMIN" },
                    { "8c577cb4-aa71-4fab-a1b0-533a3f6dabcf", null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "AuthorId", "Bio", "Name" },
                values: new object[,]
                {
                    { 1, "Nietzsche's writing spans philosophical polemics, poetry, cultural criticism, and fiction while displaying a fondness for aphorism and irony. Prominent elements of his philosophy include his radical critique of truth in favour of perspectivism; a genealogical critique of religion and Christian morality and a related theory of master–slave morality; the aesthetic affirmation of life in response to both the \"death of God\" and the profound crisis of nihilism; the notion of Apollonian and Dionysian forces; and a characterisation of the human subject as the expression of competing wills, collectively understood as the will to power. He also developed influential concepts such as the Übermensch and his doctrine of eternal return. In his later work, he became increasingly preoccupied with the creative powers of the individual to overcome cultural and moral mores in pursuit of new values and aesthetic health. His body of work touched a wide range of topics, including art, philology, history, music, religion, tragedy, culture, and science, and drew inspiration from Greek tragedy as well as figures such as Zoroaster, Arthur Schopenhauer, Ralph Waldo Emerson, Richard Wagner and Johann Wolfgang von Goethe", "Friedrich Nietzsche" },
                    { 2, "Aristotle (ˈærɪstɒtəl;[1] Greek: Ἀριστοτέλης Aristotélēs, pronounced [aristotélɛːs]; 384–322 BC) was an Ancient Greek philosopher and polymath. His writings cover a broad range of subjects spanning the natural sciences, philosophy, linguistics, economics, politics, psychology and the arts. As the founder of the Peripatetic school of philosophy in the Lyceum in Athens, he began the wider Aristotelian tradition that followed, which set the groundwork for the development of modern science.", "Aristotle" },
                    { 3, "Martin Heidegger (/ˈhaɪdɛɡər, ˈhaɪdɪɡər/;[1] German: [ˈmaʁtiːn ˈhaɪdɛɡɐ];[1] 26 September 1889 – 26 May 1976) was a German philosopher who is best known for contributions to phenomenology, hermeneutics, and existentialism. He is often considered to be among the most important and influential philosophers of the 20th century. He has been widely criticized for supporting the Nazi Party after his election as rector at the University of Freiburg in 1933, and there has been controversy about the relationship between his philosophy and Nazism.", "Martin Heidegger" },
                    { 4, "Immanuel Kant[a] (22 April 1724 – 12 February 1804) was a German philosopher and one of the central Enlightenment thinkers. Born in Königsberg, Kant's comprehensive and systematic works in epistemology, metaphysics, ethics, and aesthetics have made him one of the most influential and controversial figures in modern Western philosophy.", "Immanuel Kant" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "BookId", "AuthorId", "Description", "FilePath", "Genres", "Photo", "Title" },
                values: new object[,]
                {
                    { 1, 1, "The main theme of Thus Spoke Zarathustra is the nature of values. The values of traditional religions involve contempt for the body and a lack of creativity. Nietzsche thinks traditional religions ultimately lead to nihilism. Nietzsche proposes a morality that is creative and life-affirming", null, "philosophy", "data:image/jpeg;base64,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", "thus spoke Zarathustra" },
                    { 2, 1, "The Will to Power (German: Der Wille zur Macht) is a book of notes drawn from the literary remains of philosopher Friedrich Nietzsche by his sister Elisabeth F?rster-Nietzsche and Peter Gast (Heinrich K?selitz). The title derived from a work that Nietzsche himself had considered writing. After Nietzsche's breakdown in 1889, and the passing of control over his literary estate to his sister Elisabeth F?", null, "philosophy", "https://m.media-amazon.com/images/I/41bv6laMOML._SL350_.jpg", "The Will to Power" },
                    { 3, 1, "This new edition is the product of a collaboration between a Germanist and a philosopher who is also a Nietzsche scholar. The translation strives not only to communicate a sense of Nietzsche's style but also to convey his meaning accurately--and thus to be an important advance on previous translations of this work. A superb set of notes ensures that Clark and Swensen's Genealogy will become the new edition of choice for classroom use.", null, "philosophy", "https://img.thriftbooks.com/api/images/m/bff90d8a975e2ee0e405bfbe785a8abe423b78f6.jpg", "On the Genealogy of Morality" },
                    { 4, 2, "The Organon (Ancient Greek: Ὄργανον, meaning \"instrument, tool, organ\") is the standard collection of Aristotle's six works on logical analysis and dialectic. The name Organon was given by Aristotle's followers, the Peripatetics, who maintained against the Stoics that Logic was \"an instrument\" of Philosophy.", null, "philosophy", "https://www.google.com/imgres?imgurl=https%3A%2F%2Fm.media-amazon.com%2Fimages%2FI%2F412Xvoz7SkL._AC_UF1000%2C1000_QL80_.jpg&tbnid=JHPoIBQbdJ6t1M&vet=12ahUKEwi5rcSapaKBAxXUGBAIHfxSBtwQMygBegQIARBY..i&imgrefurl=https%3A%2F%2Fwww.amazon.com%2FOrganon-Aristotle%2Fdp%2F3849692949&docid=ksIoAKTiKAo3KM&w=666&h=1000&q=artistotle%20Organon&ved=2ahUKEwi5rcSapaKBAxXUGBAIHfxSBtwQMygBegQIARBY", "Organon" },
                    { 5, 2, "The Categories (Greek Κατηγορίαι Katēgoriai; Latin Categoriae or Praedicamenta) is a text from Aristotle's Organon that enumerates all the possible kinds of things that can be the subject or the predicate of a proposition. They are \"perhaps the single most heavily discussed of all Aristotelian notions\".[1] The work is brief enough to be divided, not into books as is usual with Aristotle's works, but into fifteen chapters.", null, "philosophy", "https://www.google.com/imgres?imgurl=https%3A%2F%2Fm.media-amazon.com%2Fimages%2FI%2F71eIjVprA1L._AC_UF1000%2C1000_QL80_.jpg&tbnid=vmyNREHeoDfTwM&vet=12ahUKEwim4PiopaKBAxWYIhAIHXH6B2kQMygCegQIARBV..i&imgrefurl=https%3A%2F%2Fwww.amazon.com%2FCategories-Aristotle%2Fdp%2F1500634689&docid=S-v1vCf_OYihIM&w=667&h=1000&q=artistotle%20categories&ved=2ahUKEwim4PiopaKBAxWYIhAIHXH6B2kQMygCegQIARBV", "Categories" },
                    { 6, 2, "Book I or Alpha begins by discussing the nature of knowledge and compares knowledge gained from the senses and from memory, arguing that knowledge is acquired from memory through experience.[6] It then defines \"wisdom\"(sophia) as a knowledge of the first principles(arche) or causes of things. Because those who are wise understand the first principles and causes, they know the why of things, unlike those who only know that things are a certain way based on their memory and sensations.", null, "philosophy", "https://www.google.com/imgres?imgurl=https%3A%2F%2Fres.cloudinary.com%2Fbloomsbury-atlas%2Fimage%2Fupload%2Fw_568%2Cc_scale%2Fjackets%2F9781780933627.jpg&tbnid=Q06Go4hhA0lPxM&vet=12ahUKEwiU2qDxpaKBAxVmJRAIHV35BRAQMygAegQIARBR..i&imgrefurl=https%3A%2F%2Fwww.bloomsbury.com%2Fus%2Falexander-of-aphrodisias-on-aristotle-metaphysics-1-9781780933627%2F&docid=thLYjI2TKzLJPM&w=568&h=853&q=artistotle%20metaphysic%201&ved=2ahUKEwiU2qDxpaKBAxVmJRAIHV35BRAQMygAegQIARBR", "Metaphysic I" },
                    { 7, 3, "Do we in our time have an answer to the question of what we really mean by the word ‘being’? Not at all. So it is fitting that we should raise anew the question of the meaning of Being. But are we nowadays even perplexed at our inability to understand the expression ‘Being’? Not at all. So first of all we must reawaken an understanding for the meaning of this question. Our aim in the following treatise is to work out the question of the meaning of Being and to do so concretely. Our provisional aim is the interpretation of time as the possible horizon for any understanding whatsoever of Being…", null, "philosophy", "https://thegreatthinkers.org/heidegger/wp-content/uploads/sites/22/2013/09/Being-and-Time-Cover.jpg", "Being and Time" },
                    { 8, 3, "What is called thinking? (German: Was heißt Denken?) is a book by the philosopher Martin Heidegger, the published version of a lecture course he gave during the winter and summer semesters of 1951 and 1952 at the University of Freiburg.", null, "philosophy", "https://thegreatthinkers.org/heidegger/wp-content/uploads/sites/22/1976/03/What-Is-Called-Thinking.jpg", "What Is Called Thinking?" },
                    { 9, 3, "On Time and Being charts the so-called “turn” in Martin Heidegger’s philosophy away from his earlier metaphysics in Being and Time to his later thoughts after “the end of philosophy.” The title lecture, “Time and Being,” shows how Heidegger reconceived both “Being” and “time,” introducing the new concept of “the event of Appropriation” to help give his metaphysical ideas nonmetaphysical meanings", null, "philosophy", "https://thegreatthinkers.org/heidegger/wp-content/uploads/sites/22/1977/03/On-Time-and-Being.jpg", "On Time and Being" },
                    { 10, 4, "A seminal text of modern philosophy, Immanuel Kant's Critique of Pure Reason (1781) made history by bringing together two opposing schools of thought: rationalism, which grounds all our knowledge in reason, and empiricism, which traces all our knowledge to experience. Published here in a lucid reworking of Max Müller's classic translation, the Critique is a profound investigation into the nature of human reason, establishing its truth, falsities, illusions, and reality.", null, "philosophy", "https://m.media-amazon.com/images/I/41VQqxjF6mL._SX324_BO1,204,203,200_.jpg", "Critique of Pure Reason" },
                    { 11, 4, "Published in 1785, Immanuel Kant's Groundwork of the Metaphysics of Morals ranks alongside Plato's Republic and Aristotle's Nicomachean Ethics as one of the most profound and influential works in moral philosophy ever written. In Kant's own words, its aim is to identify and corroborate the supreme principle of morality, the categorical imperative. He argues that human beings are ends in themselves, never to be used by anyone merely as a means, and that universal and unconditional obligations must be understood as an expression of the human capacity for autonomy and self-governance", null, "philosophy", "https://m.media-amazon.com/images/I/41WgVkPDytL._SX331_BO1,204,203,200_.jpg", "Groundwork of the Metaphysics of Morals" },
                    { 12, 4, "In \"Perpetual Peace,\" Kant discusses the conditions necessary for achieving and maintaining lasting peace among nations. He outlines several principles and ideas that he believes are essential for the establishment of a just and enduring peace.", null, "philosophy", "https://m.media-amazon.com/images/I/41s9S5ERj4L._SX332_BO1,204,203,200_.jpg", "Perpetual Peace and Other Essays" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Books_AuthorId",
                table: "Books",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_BookId",
                table: "Comments",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBooks_BookId",
                table: "UserBooks",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBooks_UserId",
                table: "UserBooks",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "UserBooks");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Authors");
        }
    }
}
