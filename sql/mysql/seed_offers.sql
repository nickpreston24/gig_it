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
from offers;