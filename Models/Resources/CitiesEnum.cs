using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Models.Resources
{
    public enum CitiesEnum
    {
        Abbotsford,
        Airdrie,
        Armstrong,
        Barrie,
        Bathurst,
        Belleville,
        Brampton,
        Brandon,
        Brant,
        Brantford,
        Brockville,
        Brooks,
        Burlington,
        Burnaby,
        Calgary,
        Cambridge,
        [Display(Name = "Campbell River")]
        CampbellRiver,
        Campbellton,
        Camrose,
        Castlegar,
        Chestermere,
        Chilliwack,
        [Display(Name = "Clarence-Rockland")]
        ClarenceRockland,
        [Display(Name = "Cold Lake")]
        ColdLake,
        Colwood,
        Coquitlam,
        [Display(Name = "Corner Brook")]
        CornerBrook,
        Cornwall,
        Courtenay,
        Cranbrook,
        Dartmouth,
        Dauphin,
        [Display(Name = "Dawson Creek")]
        DawsonCreek,
        Delta,
        Dieppe,
        Dryden,
        Duncan,
        Edmonton,
        Edmundston,
        [Display(Name = "Elliot Lake")]
        ElliotLake,
        Enderby,
        Fernie,
        [Display(Name = "Flin Flon")]
        FlinFlon,
        [Display(Name = "Fort Saskatchewan")]
        FortSaskatchewan,
        [Display(Name = "Fort St. John")]
        FortStJohn,
        Fredericton,
        [Display(Name = "Grand Forks")]
        GrandForks,
        [Display(Name = "Grand Prarie")]
        GrandePrairie,
        [Display(Name = "Greater Sudbury")]
        GreaterSudbury,
        Greenwood,
        Guelph,
        [Display(Name = "Haldimand County")]
        HaldimandCounty,
        Halifax,
        Hamilton,
        Iqaluit,
        Kamloops,
        [Display(Name = "Kawartha Lakes")]
        KawarthaLakes,
        Kelowna,
        Kenora,
        Kimberley,
        Kingston,
        Kitchener,
        Lacombe,
        Langford,
        Langley,
        Leduc,
        Lethbridge,
        Lloydminster,
        London,
        [Display(Name = "Maple Ridge")]
        MapleRidge,
        Markham,
        [Display(Name = "Medicine Hat")]
        MedicineHat,
        Merritt,
        Miramichi,
        Mississauga,
        Moncton,
        Morden,
        [Display(Name = "Mount Pearl")]
        MountPearl,
        Nanaimo,
        Nelson,
        [Display(Name = "New Westminster")]
        NewWestminster,
        [Display(Name = "Niagara Falls")]
        NiagaraFalls,
        [Display(Name = "Norfolk County")]
        NorfolkCounty,
        [Display(Name = "North Bay")]
        NorthBay,
        [Display(Name = "North Vancouver")]
        NorthVancouver,
        Orillia,
        Oshawa,
        Ottawa,
        [Display(Name = "Owen Sound")]
        OwenSound,
        Parksville,
        Pembroke,
        Penticton,
        Peterborough,
        Pickering,
        [Display(Name = "Pitt Meadows")]
        PittMeadows,
        [Display(Name = "Port Alberni")]
        PortAlberni,
        [Display(Name = "Port Colborne")]
        PortColborne,
        [Display(Name = "Port Coquitlam")]
        PortCoquitlam,
        [Display(Name = "Port Moody")]
        PortMoody,
        [Display(Name = "Portage la Prairie")]
        PortagelaPrairie,
        [Display(Name = "Powell River")]
        PowellRiver,
        [Display(Name = "Prince Edward County")]
        PrinceEdwardCounty,
        [Display(Name = "Prince George")]
        PrinceGeorge,
        [Display(Name = "Prince Rupert")]
        PrinceRupert,
        Quesnel,
        [Display(Name = "Quinte West")]
        QuinteWest,
        [Display(Name = "Red Deer")]
        RedDeer,
        Revelstoke,
        Richmond,
        Rossland,
        [Display(Name = "Saint John")]
        SaintJohn,
        [Display(Name = "Salmon Arm")]
        SalmonArm,
        Sarnia,
        [Display(Name = "Sault Ste. Marie")]
        SaultSteMarie,
        Selkirk,
        [Display(Name = "Spruce Grove")]
        SpruceGrove,
        [Display(Name = "St. Albert")]
        StAlbert,
        [Display(Name = "St. Catharines")]
        StCatharines,
        [Display(Name = "St. John's")]
        StJohns,
        [Display(Name = "St. Thomas")]
        StThomas,
        Steinbach,
        Stratford,
        Surrey,
        Sydney,
        [Display(Name = "Temiskaming Shores")]
        TemiskamingShores,
        Terrace,
        Thompson,
        Thorold,
        [Display(Name = "Thunder Bay")]
        ThunderBay,
        Timmins,
        Toronto,
        Trail,
        [Display(Name = "Vancouver")]
        Vancouver,
        Vaughan,
        Vernon,
        Victoria,
        Waterloo,
        Welland,
        [Display(Name = "West Kelowna")]
        WestKelowna,
        Wetaskiwin,
        [Display(Name = "White Rock")]
        WhiteRock,
        [Display(Name = "Williams Lake")]
        WilliamsLake,
        Windsor,
        Winkler,
        Winnipeg,
        Woodstock,
        Yellowknife
    }
}
