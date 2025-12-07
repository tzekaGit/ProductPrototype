namespace ProductPrototype.DbAccess.Data.Struct
{
    public struct ProductTemplates
    {
        public string TitleBackground = "";
        public string TableHeaderBackgroud = "";
        public ProductTemplates()
        {
            TitleBackground = "./Images/phPicnic.jpg";
            TableHeaderBackgroud = "./Images/pbPark.jpg"; //"./Images/pbPicnic.jpg";
        }


    }
}
