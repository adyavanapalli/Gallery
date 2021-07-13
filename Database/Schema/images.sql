CREATE TABLE images (
    id                      bigserial   PRIMARY KEY,
    "name"                  text        NOT NULL,
    image_pixel_data        bytea       NOT NULL,
    thumbnail_pixel_data    bytea       NOT NULL
);

COMMENT ON TABLE  images                        IS 'A table containing data about images.';
COMMENT ON COLUMN images.id                     IS 'The primary key for this table.';
COMMENT ON COLUMN images."name"                 IS 'The name of the image.';
COMMENT ON COLUMN images.image_pixel_data       IS 'The underlying pixel data for the image.';
COMMENT ON COLUMN images.thumbnail_pixel_data   IS 'The underlying pixel data for the image''s thumbnail.';
