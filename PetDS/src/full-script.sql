CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    migration_id character varying(150) NOT NULL,
    product_version character varying(32) NOT NULL,
    CONSTRAINT pk___ef_migrations_history PRIMARY KEY (migration_id)
);

START TRANSACTION;
CREATE TABLE departaments (
    id uuid NOT NULL,
    name_value_name text NOT NULL,
    identifier_value_identifier character varying(150) NOT NULL,
    parent_id uuid,
    path_value_pash text NOT NULL,
    depth smallint NOT NULL,
    is_active boolean NOT NULL DEFAULT TRUE,
    create_at timestamp with time zone NOT NULL,
    update_at timestamp with time zone NOT NULL,
    location_id uuid NOT NULL,
    CONSTRAINT pk_departaments PRIMARY KEY (id),
    CONSTRAINT fk_departaments_departaments_parent_id FOREIGN KEY (parent_id) REFERENCES departaments (id)
);

CREATE TABLE locations (
    id uuid NOT NULL,
    name_value_name text NOT NULL,
    timezone_lana_code text NOT NULL,
    is_active boolean NOT NULL DEFAULT TRUE,
    create_at timestamp with time zone NOT NULL,
    update_at timestamp with time zone NOT NULL,
    "Address" jsonb NOT NULL,
    CONSTRAINT pk_locations PRIMARY KEY (id)
);

CREATE TABLE positions (
    id uuid NOT NULL,
    name_value_name text NOT NULL,
    discription_value_discription text,
    is_active boolean NOT NULL DEFAULT TRUE,
    create_at timestamp with time zone NOT NULL,
    update_at timestamp with time zone NOT NULL,
    CONSTRAINT pk_positions PRIMARY KEY (id)
);

CREATE TABLE "departamentLocations" (
    id uuid NOT NULL,
    departament_id uuid NOT NULL,
    departament_id1 uuid NOT NULL,
    location_id uuid NOT NULL,
    CONSTRAINT pk_departament_locations PRIMARY KEY (id),
    CONSTRAINT fk_departament_locations_departaments_departament_id FOREIGN KEY (departament_id) REFERENCES departaments (id) ON DELETE CASCADE,
    CONSTRAINT fk_departament_locations_departaments_departament_id1 FOREIGN KEY (departament_id1) REFERENCES departaments (id) ON DELETE CASCADE,
    CONSTRAINT fk_departament_locations_locations_location_id FOREIGN KEY (location_id) REFERENCES locations (id) ON DELETE CASCADE
);

CREATE TABLE "departamentPositions" (
    id uuid NOT NULL,
    departament_id uuid NOT NULL,
    departament_id1 uuid NOT NULL,
    position_id uuid NOT NULL,
    CONSTRAINT pk_departament_positions PRIMARY KEY (id),
    CONSTRAINT fk_departament_positions_departaments_departament_id FOREIGN KEY (departament_id) REFERENCES departaments (id) ON DELETE CASCADE,
    CONSTRAINT fk_departament_positions_departaments_departament_id1 FOREIGN KEY (departament_id1) REFERENCES departaments (id) ON DELETE CASCADE,
    CONSTRAINT fk_departament_positions_positions_position_id FOREIGN KEY (position_id) REFERENCES positions (id) ON DELETE CASCADE
);

CREATE INDEX ix_departament_locations_departament_id ON "departamentLocations" (departament_id);

CREATE INDEX ix_departament_locations_departament_id1 ON "departamentLocations" (departament_id1);

CREATE INDEX ix_departament_locations_location_id ON "departamentLocations" (location_id);

CREATE INDEX ix_departament_positions_departament_id ON "departamentPositions" (departament_id);

CREATE INDEX ix_departament_positions_departament_id1 ON "departamentPositions" (departament_id1);

CREATE INDEX ix_departament_positions_position_id ON "departamentPositions" (position_id);

CREATE UNIQUE INDEX ix_departaments_name_value_name ON departaments (name_value_name);

CREATE INDEX ix_departaments_parent_id ON departaments (parent_id);

CREATE UNIQUE INDEX ix_locations_name_value_name ON locations (name_value_name);

CREATE UNIQUE INDEX ix_positions_name_value_name ON positions (name_value_name);

INSERT INTO "__EFMigrationsHistory" (migration_id, product_version)
VALUES ('20250924201543_Initial', '9.0.9');

COMMIT;

