﻿using DevFreela.Application.InputModels;
using DevFreela.Application.ViewModels;

namespace DevFreela.Application.Services.Interfaces
{
    public interface IProjectService
    {
        List<ProjectViewModel> GetAll();
        ProjectDetailsViewModel GetById(int id);
        int Create(NewProjectInputModel inputModel);
        int Update(UpdateProjectInputModel inputModel);
        void Delete(int id);
        void CreateComment(CreateCommentInputModel inputModel);
        void Start(int id);
        void Finish(int id);
    }
}
