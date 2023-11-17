export class GenericResponse {
  hasItems: boolean;
  items: any[];
  page: number;
  pages: number;
  total: number;

  constructor(
    hasItems: boolean,
    items: any[],
    page: number,
    pages: number,
    total: number) {
    this.hasItems = hasItems;
    this.items = [];
    this.page = page;
    this.pages = pages;
    this.total = total;
  }

}

