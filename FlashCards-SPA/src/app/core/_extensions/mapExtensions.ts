import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class MapExtensions {

  mapLanguageLevelToString(langLevel: number): string {
    switch (langLevel) {
      case 0: return 'A1';
      case 1: return 'A2';
      case 2: return 'B1';
      case 3: return 'B2';
      case 4: return 'B2/C1';
      case 5: return 'C1';
      case 6: return 'C2';
      case 7: return 'Not specified';
      default: return 'Not specified';
    }
  }

  mapLanguageLevelToNumber(langLevel: string): number {
    switch (langLevel) {
      case 'A1': return 0;
      case 'A2': return 1;
      case 'B1': return 2;
      case 'B2': return 3;
      case 'B2/C1': return 4;
      case 'C1': return 5;
      case 'C2': return 6;
      case 'Not specified': return 7;
      default: return 7;
    }
  }
}
