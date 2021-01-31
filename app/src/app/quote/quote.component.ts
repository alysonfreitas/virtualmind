import { Component, OnInit } from '@angular/core';
import { ApiService } from '../services/api.service';
import Swal from 'sweetalert2'

@Component({
  selector: 'app-quote',
  templateUrl: './quote.component.html',
  styleUrls: ['./quote.component.scss']
})
export class QuoteComponent implements OnInit {

  public isos = ["USD", "BRL"];
  public currencies: any[] = [];
  public loading = false;

  constructor(private api: ApiService) { }

  ngOnInit(): void {
    this.fetchCurrencies();
  }

  fetchCurrencies() {
    this.currencies = [];
    this.loading = true;
    for (const iso of this.isos) {
      this.api.getCurrency(iso).subscribe(currency => {
        this.loading = false;
        this.currencies.push(currency);
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

}
