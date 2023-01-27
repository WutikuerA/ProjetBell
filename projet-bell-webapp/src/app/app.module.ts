import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule, routingComponents } from './app-routing.module';
import { AppComponent } from './app.component';
import { AssetComponent } from './components/asset/asset.component';
import { InvoiceComponent } from './components/invoice/invoice.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { DataService } from 'src/data-service/data-service';
import { AssetDataService } from 'src/data-service/asset.service';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ToastrModule } from 'ngx-toastr';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { FormsModule } from '@angular/forms';


@NgModule({
  declarations: [
    routingComponents,
    AssetComponent,
    InvoiceComponent
  ],
  imports: [
    BrowserModule,
    NgbModule,
    FormsModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    FontAwesomeModule    
  ],
  providers: [
    DataService,
    AssetDataService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
