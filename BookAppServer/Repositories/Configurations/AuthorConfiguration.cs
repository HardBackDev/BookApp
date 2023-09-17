using BookAppServer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookAppServer.Repositories.Configurations
{
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.HasData(
                new Author()
                {
                    Id = 1,
                    Name = "Friedrich Nietzsche",
                    Bio = "Nietzsche's writing spans philosophical polemics, poetry, cultural criticism, and fiction while displaying a fondness for aphorism and irony." +
                    " Prominent elements of his philosophy include his radical critique of truth in favour of perspectivism;" +
                    " a genealogical critique of religion and Christian morality and a related theory of master–slave morality;" +
                    " the aesthetic affirmation of life in response to both the \"death of God\" and the profound crisis of nihilism;" +
                    " the notion of Apollonian and Dionysian forces; and a characterisation of the human subject as the expression of competing wills," +
                    " collectively understood as the will to power. He also developed influential concepts such as the Übermensch and his doctrine of eternal return." +
                    " In his later work, he became increasingly preoccupied with the creative powers of the individual to overcome cultural and moral mores in pursuit" +
                    " of new values and aesthetic health. His body of work touched a wide range of topics, including art, philology, history, music, religion, tragedy, culture," +
                    " and science, and drew inspiration from Greek tragedy as well as figures such as Zoroaster, Arthur Schopenhauer, Ralph Waldo Emerson," +
                    " Richard Wagner and Johann Wolfgang von Goethe"
                });

            builder.HasData(
                new Author()
                {
                    Id = 2,
                    Name = "Aristotle",
                    Bio = "Aristotle (ˈærɪstɒtəl;[1] Greek: Ἀριστοτέλης Aristotélēs, pronounced [aristotélɛːs]; 384–322 BC) was an Ancient Greek philosopher and polymath." +
                    " His writings cover a broad range of subjects spanning the natural sciences, philosophy, linguistics, economics, politics, psychology and the arts." +
                    " As the founder of the Peripatetic school of philosophy in the Lyceum in Athens, he began the wider Aristotelian tradition that followed," +
                    " which set the groundwork for the development of modern science."
                });

            builder.HasData(
                new Author()
                {
                    Id = 3,
                    Name = "Martin Heidegger",
                    Bio = "Martin Heidegger (/ˈhaɪdɛɡər, ˈhaɪdɪɡər/;[1] German: [ˈmaʁtiːn ˈhaɪdɛɡɐ];[1] 26 September 1889 – 26 May 1976) " +
                    "was a German philosopher who is best known for contributions to phenomenology, hermeneutics, and existentialism." +
                    " He is often considered to be among the most important and influential philosophers of the 20th century." +
                    " He has been widely criticized for supporting the Nazi Party after his election as rector at the University of Freiburg in 1933," +
                    " and there has been controversy about the relationship between his philosophy and Nazism."
                });

            builder.HasData(
                new Author()
                {
                    Id = 4,
                    Name = "Immanuel Kant",
                    Bio = "Immanuel Kant[a] (22 April 1724 – 12 February 1804) was a German philosopher and one of the central Enlightenment thinkers." +
                    " Born in Königsberg, Kant's comprehensive and systematic works in epistemology, metaphysics, ethics, and aesthetics have made him one of the most" +
                    " influential and controversial figures in modern Western philosophy."
                });
        }
    }
}
