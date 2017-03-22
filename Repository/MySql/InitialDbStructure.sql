Drop database wwsa;
Create database wwsa;
use wwsa;

create table youtube_recommendations 
(id varchar(36) NOT NULL PRIMARY KEY,
channel_name varchar(100),
description varchar(255),
url varchar(56),
image_url varchar(250),
date_created timestamp default now(), 
date_updated timestamp default now() on update now()) comment 'Used to store links and descriptions of recommended youtube channels';

create table suppliers 
(id varchar(36) NOT NULL PRIMARY KEY,
supplier_name varchar(100),
description varchar(255),
url varchar(56),
image_url varchar(250),
supplier_marker_color varchar(6),
date_created timestamp default now(), 
date_updated timestamp default now() on update now()) comment 'Used to store general tool and wood suppliers information';

create table suppliers_locations
(id varchar(36) NOT NULL PRIMARY KEY,
supplier_id varchar(100),
physical_address varchar(255),
telephone_number varchar(10),
lng float(10,6),
lat float(10,6),
date_created timestamp default now(), 
date_updated timestamp default now() on update now(),
FOREIGN KEY (supplier_id) REFERENCES suppliers(id)) comment 'Used to store additional supplier information';

create table tools
(id varchar(36) NOT NULL PRIMARY KEY,
tool_name varchar(100),
description varchar(2000),
date_created timestamp default now(), 
date_updated timestamp default now() on update now()) comment 'Used to store tool descriptions and information';


INSERT INTO wwsa.suppliers
(id, supplier_name, description, url, image_url,supplier_marker_color, date_created, date_updated)
VALUES('42c5e7cd-03c6-4eb5-b114-ee808e4d9765', 'Gelmar', 'Gelmar Handle and Furniture Fittings has an extensive range of well-priced handles.', 'http://www.gelmar.co.za', 'http://www.gelmar.co.za/image/data/gelmar_logo2.jpg',  '5DBCD2',CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);

INSERT INTO wwsa.suppliers
(id, supplier_name, description, url, image_url,supplier_marker_color, date_created, date_updated)
VALUES('5b7e841a-412e-4d45-89a4-a42e3d655f66', 'Adendorff', 'Adendorff Machinery Mart we source equipment from all over the world to offer you the greatest range of products at the best prices.', 'http://www.adendorff.co.za', 'http://www.adendorff.co.za/images/global/Adendorff-machinery-mart-logo.png', '38437F',CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);

INSERT INTO wwsa.suppliers
(id, supplier_name, description, url, image_url,supplier_marker_color, date_created, date_updated)
VALUES('a135056a-9f2f-445c-8f4e-a5c34b6306a5', 'Silverton Houthandelaars', 'Silverton Timber Merchant (PTY) LTD has been buying and selling a wide variety of timbers since 1982.', 'http://www.timbermerchant.co.za', 'http://www.yellowp.co.za/assets/img_empresa/s/silverton-houthandelaars-pty-ltd-14584.gif',  '6A2F04',CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);

INSERT INTO wwsa.suppliers
(id, supplier_name, description, url, image_url,supplier_marker_color, date_created, date_updated)
VALUES('e323ce88-86fc-4ec3-927c-5b8e09eeb739', 'Country Woods', 'Supplies more than 70 species of imported and African exotic wood, available in bundle or break bundle form to cater individuals need around South Africa.', 'http://www.countrywoods.co.za', 'http://www.snupit.co.za/content/photos/248605/logo-248605-0ac7ad82-2228-41b6-b8e3-643be50fa780.png',  '0C3A10',CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);

INSERT INTO wwsa.suppliers
(id, supplier_name, description, url, image_url,supplier_marker_color, date_created, date_updated)
VALUES('9f06e67e-8c47-48ef-84b2-7bc152697511', 'Hardware Centre', 'Hardware Centre is South Africas leading woodworkers outlet store.', 'http://www.hardwarecentre.co.za', 'http://www.hardwarecentre.co.za/web/img/Hardware%20centre%20logo.jpg',  '0060AD',CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);




INSERT INTO wwsa.suppliers_locations
(id, supplier_id, physical_address, telephone_number, lng, lat, date_created, date_updated)
VALUES((SELECT uuid()), 'a135056a-9f2f-445c-8f4e-a5c34b6306a5', '266 Plantation st Silverton 0127', '0128043110', -25.729374, 28.295220, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);

INSERT INTO wwsa.suppliers_locations
(id, supplier_id, physical_address, telephone_number, lng, lat, date_created, date_updated)
VALUES((SELECT uuid()), '9f06e67e-8c47-48ef-84b2-7bc152697511', 'Malibongwe Dr, Johannesburg', '0117910844', -26.080142, 27.978123, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);




INSERT INTO wwsa.youtube_recommendations
(id, channel_name, description, url, image_url, date_created, date_updated)
VALUES( (SELECT uuid()), 'Frank Howarth', 'Architecture at a small scale expressed through woodworking and film making.', 'https://www.youtube.com/channel/UC3_VCOJMaivgcGqPCTePLBA', 'https://yt3.ggpht.com/-GMkkUpV4PX0/AAAAAAAAAAI/AAAAAAAAAAA/bJaQNN3eAXs/s100-c-k-no-mo-rj-c0xffffff/photo.jpg', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);

INSERT INTO wwsa.youtube_recommendations
(id, channel_name, description, url, image_url, date_created, date_updated)
VALUES( (SELECT uuid()), 'Maurice Blok', 'Wood & such. Join me. Have fun. If any, leave your comments or questions.', 'https://www.youtube.com/channel/UCc_fQ3YbIwYL2YV0iuEJNYQ', 'https://yt3.ggpht.com/-jmHGUHJyIao/AAAAAAAAAAI/AAAAAAAAAAA/ChMdKsbB39A/s100-c-k-no-mo-rj-c0xffffff/photo.jpg', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);

INSERT INTO wwsa.youtube_recommendations
(id, channel_name, description, url, image_url, date_created, date_updated)
VALUES( (SELECT uuid()), 'Woodworkers Journal', 'Woodworkers Journal is Americas leading woodworking authority.', 'https://www.youtube.com/channel/UC84kr2zOvY6Pyfnv8yYdQRA', 'https://yt3.ggpht.com/-yRDqV39B4Q4/AAAAAAAAAAI/AAAAAAAAAAA/DZ2-aPyjctI/s100-c-k-no-mo-rj-c0xffffff/photo.jpg', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);

INSERT INTO wwsa.youtube_recommendations
(id, channel_name, description, url, image_url, date_created, date_updated)
VALUES( (SELECT uuid()), 'Steve Ramsey', 'Woodworking for Mere Mortals is dedicated to making woodworking easy, fun and accessible.', 'https://www.youtube.com/channel/UCBB7sYb14uBtk8UqSQYc9-w', 'https://yt3.ggpht.com/-60UeHmwKfZg/AAAAAAAAAAI/AAAAAAAAAAA/vKU1lfJfd1A/s100-c-k-no-mo-rj-c0xffffff/photo.jpg', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);

INSERT INTO wwsa.youtube_recommendations
(id, channel_name, description, url, image_url, date_created, date_updated)
VALUES( (SELECT uuid()), 'April Wilkerson', 'I am an obsessed DIYer and Woodworker. Im not professional or have any training, so I just pick the project I want to tackle and figure it out step by step.', 'https://www.youtube.com/channel/UC4v2tQ8GqP0RbmAzhp4IFkQ', 'https://yt3.ggpht.com/-Q2JA-AIO-ck/AAAAAAAAAAI/AAAAAAAAAAA/EKQwaVl2_a4/s100-c-k-no-mo-rj-c0xffffff/photo.jpg', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);

INSERT INTO wwsa.youtube_recommendations
(id, channel_name, description, url, image_url, date_created, date_updated)
VALUES( (SELECT uuid()), 'WoodWork Web', 'Welcome to Woodworkweb TV, the interactive resource for all woodworkers.', 'https://www.youtube.com/channel/UCMd9JgqSESwyuOs0yuSTlEw', 'https://yt3.ggpht.com/-zVbg9tHvtyU/AAAAAAAAAAI/AAAAAAAAAAA/hzCQSdl44Kk/s100-c-k-no-mo-rj-c0xffffff/photo.jpg', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);

INSERT INTO wwsa.youtube_recommendations
(id, channel_name, description, url, image_url, date_created, date_updated)
VALUES( (SELECT uuid()), 'Jay Bates', 'Jays Custom Creations LLC', 'https://www.youtube.com/channel/UC-7XY-W_C84cW2MNqujgFpg', 'https://yt3.ggpht.com/-b5kAO6E7LSQ/AAAAAAAAAAI/AAAAAAAAAAA/3l71ZJYDGjE/s100-c-k-no-mo-rj-c0xffffff/photo.jpg', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);

INSERT INTO wwsa.youtube_recommendations
(id, channel_name, description, url, image_url, date_created, date_updated)
VALUES( (SELECT uuid()), 'Chris Salomone', 'I love designing things..and I tolerate building them.', 'https://www.youtube.com/channel/UC1V-DYqsaj764uBis9-UDug', 'https://yt3.ggpht.com/-xmUq1xPrpgQ/AAAAAAAAAAI/AAAAAAAAAAA/wsXLLhNIuTc/s100-c-k-no-mo-rj-c0xffffff/photo.jpg', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);

INSERT INTO wwsa.youtube_recommendations
(id, channel_name, description, url, image_url, date_created, date_updated)
VALUES( (SELECT uuid()), 'mtmwood', 'My name is Andrei. I live in Russia and run my small business. Woodworking is my passion.', 'https://www.youtube.com/channel/UCWBTyvNhUXq0ofu6ta1EAaQ', 'https://yt3.ggpht.com/-94XfWeIrrzQ/AAAAAAAAAAI/AAAAAAAAAAA/4KfUwwTvKxM/s100-c-k-no-mo-rj-c0xffffff/photo.jpg', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);

INSERT INTO wwsa.youtube_recommendations
(id, channel_name, description, url, image_url, date_created, date_updated)
VALUES( (SELECT uuid()), 'I make stuff', 'I like to make all sorts of stuff, with all sorts of materials. I have lots of projects including woodworking, metalworking, electronics, 3D printing, prop making and more!', 'https://www.youtube.com/user/iliketomakestuffcom', 'https://yt3.ggpht.com/-kVJyRVXzd7E/AAAAAAAAAAI/AAAAAAAAAAA/jFcJXJDNz68/s100-c-k-no-mo-rj-c0xffffff/photo.jpg', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);

INSERT INTO wwsa.youtube_recommendations
(id, channel_name, description, url, image_url, date_created, date_updated)
VALUES( (SELECT uuid()), 'Matthias Wandel', 'Videos about woodworking, taking more of an engineering perspective on things.', 'https://www.youtube.com/channel/UCckETVOT59aYw80B36aP9vw', 'https://yt3.ggpht.com/-Z5sjly2fAWA/AAAAAAAAAAI/AAAAAAAAAAA/fvVZkEvk99c/s100-c-k-no-mo-rj-c0xffffff/photo.jpg', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);

INSERT INTO wwsa.youtube_recommendations
(id, channel_name, description, url, image_url, date_created, date_updated)
VALUES( (SELECT uuid()), 'John Heisz', 'Videos on woodworking and workshop related projects.', 'https://www.youtube.com/user/jpheisz/videos', 'https://yt3.ggpht.com/-s1iavblFQoE/AAAAAAAAAAI/AAAAAAAAAAA/GNe5z6utYU0/s100-c-k-no-mo-rj-c0xffffff/photo.jpg', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);


INSERT INTO wwsa.tools
(id, tool_name, description, date_created, date_updated)
VALUES((SELECT uuid()), 'Table Saw', 'Some sescription required..', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);

INSERT INTO wwsa.tools
(id, tool_name, description, date_created, date_updated)
VALUES((SELECT uuid()), 'Router', 'Some sescription required..', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);

INSERT INTO wwsa.tools
(id, tool_name, description, date_created, date_updated)
VALUES((SELECT uuid()), 'Router Table', 'Some sescription required..', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);

INSERT INTO wwsa.tools
(id, tool_name, description, date_created, date_updated)
VALUES((SELECT uuid()), 'Bandsaw', 'Some sescription required..', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);

INSERT INTO wwsa.tools
(id, tool_name, description, date_created, date_updated)
VALUES((SELECT uuid()), 'Scroll Saw', 'Some sescription required..', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);

INSERT INTO wwsa.tools
(id, tool_name, description, date_created, date_updated)
VALUES((SELECT uuid()), 'Jointer', 'Some sescription required..', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);

INSERT INTO wwsa.tools
(id, tool_name, description, date_created, date_updated)
VALUES((SELECT uuid()), 'Planer', 'Some sescription required..', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);


INSERT INTO wwsa.tools
(id, tool_name, description, date_created, date_updated)
VALUES((SELECT uuid()), 'Hand Plane', 'Some sescription required..', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);


INSERT INTO wwsa.tools
(id, tool_name, description, date_created, date_updated)
VALUES((SELECT uuid()), 'Sander', 'Some sescription required..', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);


INSERT INTO wwsa.tools
(id, tool_name, description, date_created, date_updated)
VALUES((SELECT uuid()), 'Clamps', 'Some sescription required..', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);


INSERT INTO wwsa.tools
(id, tool_name, description, date_created, date_updated)
VALUES((SELECT uuid()), 'Glue', 'Some sescription required..', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);


INSERT INTO wwsa.tools
(id, tool_name, description, date_created, date_updated)
VALUES((SELECT uuid()), 'Mitre Saw', 'Some sescription required..', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);


INSERT INTO wwsa.tools
(id, tool_name, description, date_created, date_updated)
VALUES((SELECT uuid()), 'Drill Press', 'Some sescription required..', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);


INSERT INTO wwsa.tools
(id, tool_name, description, date_created, date_updated)
VALUES((SELECT uuid()), 'Compressor', 'Some sescription required..', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);





