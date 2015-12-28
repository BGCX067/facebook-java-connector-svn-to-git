using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Rage.Addservice.ViewModel.Base;
using System.Collections.ObjectModel;
using Rage.Addservice.Service;
using Rage.Addservice.Infrastructure;
using Microsoft.Practices.Prism.Commands;
using Rage.Addservice.Domain.Model;
using Rage.Addservice.Utill;
using System.Collections.Generic;
using System.Linq;

namespace Rage.Addservice.ViewModel
{
    public class AdvertsContainerViewModel : ViewModelBase
    {

        private readonly IPersistenceService persistenceService;
        private readonly IViewConductor viewConductor;
        private readonly IObservable<ObservableCollection<AdvertViewModel>> adverts = new Observable<ObservableCollection<AdvertViewModel>>(new ObservableCollection<AdvertViewModel>());
        private readonly IObservable<ObservableCollection<WallViewModel>> myWalls = new Observable<ObservableCollection<WallViewModel>>(new ObservableCollection<WallViewModel>());

        private Observable<WallViewModel> selectedWall = new Observable<WallViewModel>();
        private Observable<AdvertViewModel> selectedAdvert = new Observable<AdvertViewModel>();

        private readonly DelegateCommand addNewCommand;
        private readonly DelegateCommand okCommand;

        public AdvertsContainerViewModel(IPersistenceService persistenceService, IViewConductor viewConductor)
        {
            this.addNewCommand = new DelegateCommand(OnAddNew);
            this.okCommand = new DelegateCommand(OnOk);

            this.persistenceService = persistenceService;
            this.viewConductor = viewConductor;
        }

        public void Initialize(List<Advert> adverts) 
        {
            if (adverts != null)
            {
                foreach (var advert in adverts)
                {
                    this.adverts.Value.Add(new AdvertViewModel(advert));
                }
            }

            this.persistenceService.GetWalls(OnGetWallsCompleted);
        }

        private void OnGetWallsCompleted(List<Wall> walls)
        {
            if (walls != null)
            {
                foreach (var wall in walls)
                {
                    myWalls.Value.Add(new WallViewModel(wall));
                }
            }

            if (myWalls.Value.Count != 0)
            {
                this.SelectedWall.Value = myWalls.Value[0];
            }
        }

        public void OnAddNew()
        {
            editableItem = new AdvertViewModel();
            this.viewConductor.Show("NewAdvertPopup", this);
        }

        public AdvertViewModel editableItem;
        public AdvertViewModel EditableItem 
        {
            get { return this.editableItem; }
            set { this.editableItem = value; }
        }


        private void OnOk()
        {
            Advert advert = this.editableItem.GetModel();
            advert.Wall = this.selectedWall.Value.GetModel();

            //if (selectedWall.Value != null)
            //{
            //    advert.Wall.Id = this.selectedWall.Value.Id;
            //}
            //else 
            //{
            //    advert.Wall.Id = 123;
            //}
            this.persistenceService.CreateAdvert(advert, OnAdded);
        }

        private void OnAdded(int? id)
        {
            if (id == null)
            {
                //TODO: Show message 
            }
            else
            {
                this.editableItem.Id.Value = id.Value;
                this.editableItem.Wall.Value = this.selectedWall;
                this.Adverts.Value.Add(editableItem);
            }
        }

        public DelegateCommand AddNewCommand
        {
            get { return this.addNewCommand; }
        }

        public DelegateCommand OkCommand 
        {
            get { return this.okCommand; }
        }

        public IObservable<ObservableCollection<AdvertViewModel>> Adverts 
        {
            get { return adverts; }
        }

        public IObservable<ObservableCollection<WallViewModel>> Walls
        {
            get { return this.myWalls; }
        }

        public Observable<WallViewModel> SelectedWall 
        {
            get { return this.selectedWall; }
            set { this.selectedWall = value; }
        }

        public Observable<AdvertViewModel> SelectedAdvert 
        {
            get { return this.selectedAdvert; }
        }

        public void SeeDetails() 
        {
            this.viewConductor.Show("AdvertDetailsPopup", this.SelectedAdvert.Value);
        }

        public void SeeSelectedWallDetails() 
        {
            this.viewConductor.Show("WallDetailsPopup", this.selectedWall.Value);
        }

        public void SeeWallDetails()
        {
            var wall = this.Walls.Value.First(w => this.selectedAdvert.Value.Wall.Value.Id.Value == w.Id.Value);

            this.viewConductor.Show("WallDetailsPopup", wall);
        }
    }
}
