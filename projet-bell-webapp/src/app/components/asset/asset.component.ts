import { Component, Renderer2, ViewChild } from '@angular/core';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { AssetDataService } from 'src/data-service/asset.service';
import { AssetFilter } from 'src/filters/asset-filter';
import { Asset } from 'src/models/asset.model';

@Component({
  selector: 'app-assets',
  templateUrl: './asset.component.html',
  styleUrls: ['./asset.component.css']
})
export class AssetComponent {

  constructor(
    private assetDataService: AssetDataService, 
    private modalService: NgbModal,
    private renderer: Renderer2,
    private toastr: ToastrService) 
  {  
  }

  model: any;
  assetList: Asset[] = [];
  previousSelectedRow: any;
  isFormValid : boolean = false;
  currentName: string = "";
  currentPrice!: number;
  currentSelectedAsset: Asset = {id: 0, name: ''};
  displayedAsset: Asset = {id: 0, name: ''};
  isCreationMode: boolean = false;
  searchKeyword: string = "";
  filter: AssetFilter = {}; 


  @ViewChild('productEditModal') assetModal!: NgbModal;
  private modalRef!: NgbModalRef;
  ngOnInit(): void 
  {    
    this.assetDataService.getAssets(this.filter).subscribe(res => 
      {
        // Get all the assets
        this.assetList = res;
      })
    this.currentSelectedAsset = {id: 0, name: ''};
    this.displayedAsset = {id: 0, name: ''};

  }  

// action bar buttons --------------------------------------------------
  onCreateClicked()
  {    
    this.isCreationMode = true;
    this.displayedAsset = {id: 0, name: ''};
    this.openModal();
  }

  onEditClicked()
  {
    this.isCreationMode = false;
    if(this.currentSelectedAsset.id == 0)
    {
      this.toastr.warning('Gotta select an asset first.', 'Oops');
    }
    else
    {
      this.displayedAsset = this.currentSelectedAsset;
      this.openModal();
    }

  }

  onDeleteClicked()
  {
    if(this.currentSelectedAsset.id == 0)
    {
      this.toastr.warning('Gotta select an asset first.', 'Oops');
    }
    else
    {
      this.displayedAsset = this.currentSelectedAsset;

      this.assetDataService.deleteAsset(this.currentSelectedAsset.id).subscribe(
        res => {
          this.toastr.success('The asset is deleted', 'Gone, nothing left.')
          this.ngOnInit();//refresh the page
        },
        err => { 
          console.log(err); 
          this.toastr.success('Somthing went wrong', 'Error occured while saving.')
        }
      );
    }

  }

  onSearchClicked()
  {
    this.filter.Name = this.searchKeyword;
    this.ngOnInit();
  }

  // -----------------Modal------------------------------------------------
  openModal()
  {
    var modal = document.getElementById("my-modal");
    this.renderer.setStyle(modal, 'visibility', 'visible');
  }

  onModalClosed()
  {
    var modal = document.getElementById("my-modal");
    this.renderer.setStyle(modal, 'visibility', 'hidden');
  }
  
  onRowSelected(i: number)
  {
    // Render selected line color change
    if(this.previousSelectedRow!=null)
    {
      this.renderer.removeClass(this.previousSelectedRow, "selected-row");
    }  

    var selectedRow = document.getElementById(i.toString());
    this.renderer.addClass(selectedRow, "selected-row");

    this.previousSelectedRow = selectedRow;

    // select Asset item
    this.currentSelectedAsset = this.assetList.find(a => a.id == i)!;
  }

  onCancel()
  {
    this.onModalClosed();
  }

  onSave()
  {
    let assetOnSave = {
      id: this.isCreationMode ? 0 : this.displayedAsset.id,
      name: this.displayedAsset.name,
      price: this.displayedAsset.price,
      validFrom: this.displayedAsset.validFrom,
      validTo: this.displayedAsset.validTo
    }

    this.assetDataService.modifyAsset(assetOnSave).subscribe(
      res => {
        this.toastr.success('Submitted successfully', 'Asset saved.')
        this.onModalClosed();
        this.ngOnInit();//refresh the page
      },
      err => { 
        console.log(err); 
        this.toastr.success('Somthing went wrong', 'Error occured while saving.')
      }
    );

  }


}
