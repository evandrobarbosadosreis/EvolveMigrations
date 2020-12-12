CREATE TABLE public.usuarios
(
    id serial,
    nome text,
    email text,
    PRIMARY KEY (id)
);

ALTER TABLE public.usuarios
    OWNER to postgres;