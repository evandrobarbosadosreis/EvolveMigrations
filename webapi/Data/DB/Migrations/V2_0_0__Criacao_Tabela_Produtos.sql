CREATE TABLE public.produtos
(
    id serial,
    nome text,
    descricao text,
    preco_venda numeric(15, 2),
    preco_custo numeric(15, 2),
    PRIMARY KEY (id)
);

ALTER TABLE public.produtos
    OWNER to postgres;