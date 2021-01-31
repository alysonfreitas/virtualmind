import { Component, OnInit } from '@angular/core';
import { ApiService } from '../services/api.service';
import Swal from 'sweetalert2'

@Component({
  selector: 'app-purchase',
  templateUrl: './purchase.component.html',
  styleUrls: ['./purchase.component.scss']
})
export class PurchaseComponent implements OnInit {

  public form = {
    userId: null,
    currencyCode: null,
    amount: '0',
  };

  public loading = false;

  constructor(private api: ApiService) { }

  ngOnInit(): void {
  }

  purchase() {
    if (!this.form.userId) {
      Swal.fire({
        icon: 'error',
        title: 'Oops...',
        text: 'User is required!'
      });
      return;
    }

    if (!this.form.currencyCode) {
      Swal.fire({
        icon: 'error',
        title: 'Oops...',
        text: 'Currency is required!'
      });
      return;
    }

    if (parseFloat(this.form.amount) <= 0) {
      Swal.fire({
        icon: 'error',
        title: 'Oops...',
        text: 'Amount must to be more than 0!'
      });
      return;
    }

    const params = { ...this.form };
    this.loading = true;
    this.api.newTransaction(params).subscribe(transaction => {
      this.loading = false;
      Swal.fire({
        icon: 'success',
        title: 'Success',
        text: `You have just purchased ${transaction.amountPurchased} ${transaction.currencyCode}!`
      });
    }, response => {
      this.loading = false;
      console.error(response.error);
      if (response.error && response.error.message) {
        Swal.fire({
          icon: 'error',
          title: 'Oops...',
          text: response.error.message
        });
      }
    });
  }

}
