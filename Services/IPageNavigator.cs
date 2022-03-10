using SnakeGame.ViewModels;
using System;

namespace SnakeGame.Services
{
    public interface IPageNavigator
    {
        event Action PageChanged;
        BaseViewModel CurrentPage { get; set; }
        void NavigateTo<T>() where T : BaseViewModel;
    }
}