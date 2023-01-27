import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { AssetComponent } from './components/asset/asset.component';
import { AppComponent } from './app.component';
import { InvoiceComponent } from './components/invoice/invoice.component';
import { ToastrModule } from 'ngx-toastr';

const routes: Routes=  [
  {path: '', redirectTo: "", pathMatch: 'full'}, // Default URL
  {path: "app-assets", component: AssetComponent},
  {path: "app-invoice", component: InvoiceComponent}
];

export const routingComponents = [
    AppComponent,
    AssetComponent,
    InvoiceComponent
];

@NgModule({
  declarations: [],

  imports: [
    CommonModule,
    RouterModule.forRoot(routes),
    ToastrModule.forRoot({
      positionClass: 'toast-bottom-right', 
      preventDuplicates: true, 
      tapToDismiss: true, 
      closeButton: true, 
      newestOnTop: true, 
      timeOut: 10000, 
      progressBar: true, 
      enableHtml: true
    }),
  ],

  exports: [RouterModule]
})

export class AppRoutingModule { }
