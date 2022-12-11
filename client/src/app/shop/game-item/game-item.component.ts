import { Component, Input } from '@angular/core';
import { IGames } from 'src/app/shared/Models/games';

@Component({
  selector: 'app-game-item',
  templateUrl: './game-item.component.html',
  styleUrls: ['./game-item.component.scss']
})
export class GameItemComponent {

@Input() game!: IGames;

}
