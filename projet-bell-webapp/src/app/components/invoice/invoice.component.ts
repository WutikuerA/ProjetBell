import { Component, Renderer2 } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { InvoiceDataService } from 'src/data-service/invoice.service';
import { InvoiceFilter } from 'src/filters/invoice-filter';
import { Invoice } from 'src/models/invoice.model';

@Component({
  selector: 'app-invoice',
  templateUrl: './invoice.component.html',
  styleUrls: ['./invoice.component.css']
})
export class InvoiceComponent {

  constructor(
    private invoiceDataService: InvoiceDataService, 
    private modalService: NgbModal,
    private renderer: Renderer2,
    private toastr: ToastrService) 
  {  
  }

  currentInvoice: Invoice = {};
  isAssetChanged : boolean = false;
  filter: InvoiceFilter = {};
  invoiceList : Invoice[] = [];
  priceAbove?:number;

  ngOnInit()
  {
    // Get invoices
    this.invoiceDataService.getInvoices(this.filter).subscribe(res => 
    {
      this.invoiceList = res;  
      var row = document.getElementById("1");
      this.renderer.setStyle(row, 'background-color', 'green');    
    });

    //Check if new invoice is available
    this.invoiceDataService.isAssetChanged().subscribe(res => 
    {
      this.isAssetChanged = res;
    });
  }

  ngAfterViewChecked()
  {
    // set the lastest invoice with green back ground
    var row = document.getElementById("0");
    if(row != null && !row.classList.contains("green-background"))
    {
      row?.classList.add("green-background")
      // this.renderer.setStyle(row, 'background-color', 'green');
    }
  }
  

  onGenerateClicked()
  {
    if(!this.isAssetChanged)
    {
      this.toastr.error('Modify or create asset to get new invoice', 'Lastest invoice is already there in green');
    }
    else
    {
      this.invoiceDataService.generateInvoice().subscribe(res => 
        {
          this.currentInvoice = res;
          this.toastr.success('Invoice generated', 'Good');
          this.ngOnInit()
        });
    }

  }

  onSearchClicked()
  {
    this.filter.priceAbove = this.priceAbove;
    this.ngOnInit();
  }

}
