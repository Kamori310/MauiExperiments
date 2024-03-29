using System.Collections.ObjectModel;

namespace NotesWithMaui.Models;

internal class AllNotes {
    public ObservableCollection<Note> Notes { get; set; } = new ObservableCollection<Note>();

    public AllNotes() =>
        LoadNotes();

    public void LoadNotes() {
        Notes.Clear();

        // Get the folder where the notes are stored.
        string appDataPath = FileSystem.AppDataDirectory;
        Console.Write(appDataPath);

        // Use Linq extensions to lead the *.notes.txt files.
        IEnumerable<Note> notes = Directory

            // Select the file names from the directory
            .EnumerateFiles(appDataPath, "*.notes.txt")

            // Each file name is used to create a new Note
            .Select(filename => new Note() {
                Filename = filename,
                Text = File.ReadAllText(filename),
                Date = File.GetCreationTime(filename)
            })

            // With the final collection of notes, order them by date
            .OrderBy(note => note.Date);

        // Add each note into the ObservableCollection
        foreach (Note note in notes) {
            Notes.Add(note);
        }
    }
}