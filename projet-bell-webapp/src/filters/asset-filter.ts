export class AssetFilter {
    Id?: number = -1
    Name? : string;
    PriceMoreThan?: number = 0;
    PriceLessThan?: number = 0;
    AfterDate?: Date;
    BeforeDate?: Date;
}