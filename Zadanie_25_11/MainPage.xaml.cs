using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace Zadanie_25_11;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        BindingContext = new TaskViewModel();
    }

    // Klasy wewnętrzne
    public class TaskItem
    {
        public string Title { get; set; }
        public bool IsCompleted { get; set; }
    }

    public class TaskViewModel : INotifyPropertyChanged
    {
        private string taskTitle;

        public string TaskTitle
        {
            get => taskTitle;
            set
            {
                if (taskTitle != value)
                {
                    taskTitle = value;
                    OnPropertyChanged(nameof(TaskTitle));
                }
            }
        }

        public ObservableCollection<TaskItem> Tasks { get; } = new();

        public int TaskCount => Tasks.Count;

        public ICommand AddTaskCommand { get; }
        public ICommand RemoveTaskCommand { get; }
        public ICommand RemoveCompletedCommand { get; }

        public TaskViewModel()
        {
            AddTaskCommand = new Command(AddTask);
            RemoveTaskCommand = new Command<TaskItem>(RemoveTask);
            RemoveCompletedCommand = new Command(RemoveCompletedTasks);
        }

        private void AddTask()
        {
            if (!string.IsNullOrWhiteSpace(TaskTitle))
            {
                Tasks.Add(new TaskItem { Title = TaskTitle, IsCompleted = false });
                TaskTitle = string.Empty;
                OnPropertyChanged(nameof(TaskCount));
            }
        }

        private void RemoveTask(TaskItem task)
        {
            if (task != null && Tasks.Contains(task))
            {
                Tasks.Remove(task);
                OnPropertyChanged(nameof(TaskCount));
            }
        }

        private void RemoveCompletedTasks()
        {
            for (int i = Tasks.Count - 1; i >= 0; i--)
            {
                if (Tasks[i].IsCompleted)
                    Tasks.RemoveAt(i);
            }
            OnPropertyChanged(nameof(TaskCount));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
