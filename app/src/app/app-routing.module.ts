import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { QuoteComponent } from './quote/quote.component';
import { PurchaseComponent } from './purchase/purchase.component';

const routes: Routes = [
  { path: '', redirectTo: 'quote', pathMatch: 'full' },
  { path: 'quote', component: QuoteComponent },
  { path: 'purchase', component: PurchaseComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
