namespace NotepadConsole
{
    using Xunit;
    using System.IO;
    using System;
    using System.Diagnostics;

    public class NotepadTests
    {
        private const string TestFilePath = "elin_not.txt";

        [Fact]
        public void NewNote_ShouldBeEmpty()
        {
            var notepad = new Notepad();
            Assert.Equal("", notepad.GetNote());
        }

        [Theory]
        [InlineData("")]
        [InlineData("Short Note")]
        [InlineData("This is a very very long note that contains a lot of characters to test the boundary conditions in the notepad application.")]
        public void EditNote_ShouldUpdateText(string text)
        {
            var notepad = new Notepad();
            notepad.EditNote(text);
            Assert.Equal(text, notepad.GetNote());
        }

        [Fact]
        public void SaveAndLoadNote_ShouldPersistData()
        {
            var notepad = new Notepad(TestFilePath);
            notepad.EditNote("Persistent data");
            notepad.SaveNote();

            var loadedNotepad = new Notepad(TestFilePath);
            loadedNotepad.LoadNote();

            Assert.Equal("Persistent data", loadedNotepad.GetNote());

            // Очистка после теста
            File.Delete(TestFilePath);
        }


    }




    internal class Program
    {
        static void Main(string[] args)
        {
            var notepade = new Notepad();
            notepade.LoadNote(); 

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Блокнот");
                Console.WriteLine("1. Создать новую заметку");
                Console.WriteLine("2. Редактировать заметку");
                Console.WriteLine("3. Показать заметку");
                Console.WriteLine("4. Сохранить и выйти");

                Console.Write("Выберите действие: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        notepade.note = "";
                        Console.WriteLine("Новая заметка создана.");
                        break;
                    case "2":
                        Console.Write("Введите текст заметки: ");
                        notepade.note = Console.ReadLine();
                        break;
                    case "3":
                        Console.WriteLine("Текущая заметка:");
                        Console.WriteLine(notepade.note);
                        Console.WriteLine("\nНажмите любую клавишу...");
                        Console.ReadKey();
                        break;
                    case "4":
                        notepade.SaveNote();
                        return;
                    default:
                        Console.WriteLine("Неверный выбор, попробуйте снова.");
                        break;
                }
            }
        }
    }

    // Минимальная реализация Notepad для тестов
    public class Notepad
    {
        public string note = "";
        public readonly string filePath;

        public Notepad(string path = "note.txt")
        {
            filePath = path;
            Debug.WriteLine("Создание экземпляра класса Notepad");
        }

        public void EditNote(string text) => note = text;
        public string GetNote() => note;
        public void SaveNote() => File.WriteAllText(filePath, note);
        public void LoadNote() => note = File.Exists(filePath) ? File.ReadAllText(filePath) : "";
    }
}


