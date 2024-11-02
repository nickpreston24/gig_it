/*
 SAmples	Offer	Distance (mi.)	Profit	% profit	$/mi.	Profit/.mi	Run Cost	% of Tank		Fuel Cost / mi	Route	Tank Cost	Tank Size	MPg	Wear and tear cost / mi.	
UberX	$56.44	87.00	$42.52	75.34%	$0.65	$0.49	-$13.92	22.25%		-$0.16	https://duckduckgo.com/?q=maps+love+field&t=canonical&iaxm=directions&end=Dallas+Love+Field%2C+8008+Cedar+Springs+Rd%2C+Dallas%2C+TX++75235%2C+United+States&transport=automobile&start=what%3ASulphur%2520Springs%252C%2520TX%2Cwhere%3AUnited%2520States	$62.90	17	23	-0.67	
 */

drop table if exists offers;
create table if not exists offers
(
    id                 INT NOT NULL AUTO_INCREMENT,
    app_name           varchar(100),
    route_url          text,
    offer              float,
    distance_mi        float,
    fuel_cost_per_mi   float,
    tank_size_gal      float,
    mpg                float, # miles per gallon
    wear_and_tear_cost float, # irs, by default

#     username           varchar(150),
#     user_id            varchar(150), // try: https://www.youtube.com/watch?v=wenGcYOfXiA

#     content       text,
    status             varchar(25),

    created_at         datetime     default now(),
    created_by         varchar(150) default null,
    last_modified      datetime     default null,
    modified_by        varchar(150) default null,

    # PK's
    PRIMARY KEY (id)
);

insert into offers (app_name, offer, distance_mi, mpg, fuel_cost_per_mi)
values ('UberEats', 6.00, 2.00, 23.00, -0.16),
       ('UberEats', 5.00, 2.00, 23.00, -0.16),
       ('UberX', 56.44, 87.00, 23.00, -0.16),
       ('Instacart', 6.50, 2.20, 23.00, -0.16),
       ('Instacart', 11.34, 4.00, 23.00, -0.16),
       ('Instacart', 11.13, 2.80, 23.00, -0.16)
;


select *
from offers
order by created_at desc;
# limit 10 offset 5
;


/*
 
 delete from offers where id > 0;
 */

### Average Offers
CREATE or replace VIEW AverageOffers AS
SELECT FORMAT(sum(offer), 2)                                                                     as offers_total,
       FORMAT(sum(case when app_name = 'UberEats' then offer else 0 end) * 100 / SUM(offer), 2)  as UberEats_Avg_Offer,
       FORMAT(sum(case when app_name = 'UberX' then offer else 0 end) * 100 / SUM(offer), 2)     as UberX_Avg_Offer,
       FORMAT(sum(case when app_name = 'Instacart' then offer else 0 end) * 100 / SUM(offer), 2) as Instacart_Avg_Offer,
       FORMAT(sum(case when app_name = 'DoorDash' then offer else 0 end) * 100 / SUM(offer), 2)  as DoorDash_Avg_Offer
FROM offers;

Select *
from AverageOffers;

/*
 Need:
 1. Average $/mi.
 2. Average cost/mi.
 3. Average Trip Distance 
 4. Average MPG
  */

CREATE or replace VIEW OffersOverView AS
SELECT FORMAT(sum(offer), 2)                                      as offers_total,
       count(offer)                                               as offer_count,
       FORMAT(sum(offer) / count(offer), 2)                       as average_offer,
       FORMAT(sum(distance_mi) / count(distance_mi), 2)           as average_trip,
       FORMAT(sum(fuel_cost_per_mi) / count(fuel_cost_per_mi), 2) as average_fuel_cost_per_mi,
       FORMAT(sum(mpg) / count(mpg), 2)                           as average_mpg
FROM offers;

select *
from OffersOverView;


# 
# CREATE or replace VIEW AboveAverageOffers AS
# SELECT offer
# FROM offers
# WHERE offer > (SELECT AVG(UberEats_Avg_Offer) FROM AverageOffers)
# # and app_name = 'UberEats'
# ;
# 
# select *
# from AboveAverageOffers;


# 
# select id
#      ,count(*)     as num_app_nameuages
#      ,sum(offer) as total_offer
#      ,sum(case when app_name = 'en' then offer else 0 end)  as en_creds
#      ,sum(case when app_name = 'fr' then offer else 0  end) as fr_creds
# from offers
# group
#     by id;


/*
 
 I learned something new last night when I was playing around with my app, something I missed when working my software jobs... And that something is Views.
Basically, Views are like having two Sheets inside an Excel workbook, and having Sheet1 hold all the raw rows of data (like your cruise finances) and Sheet2 having all the major calculations, like maybe 'average cabin cost', or 'average trip length'.
This is the code I wrote last night to do the same thing, but for my future user's trips.
Notice the stuff on the right after the purple 'as' text: (average_offer, average_mpg, etc.).  Those are values I wanna show my users to help them pick better dashes.
Normally, I'd have these calculated every time the user requests them.  But by declaring this code as a View (see the top line), each of those calculations you see below (e.g. sum(mpg)/count(mpg)) are automatically calculated for the entire data set, every time a User of mine submits a new dash offer!  
 */