drop table POOL;

create table POOL
(
    POOL_ID          RAW(16) not null
        constraint POOL_PK
            primary key
        constraint POO_POOL_ID_NN
            check ("POOL_ID" IS NOT NULL),
    MASTER_POOL_ID   RAW(16) not null
        constraint POO_MASTER_POOL_FK
            references MASTER_POOL
        constraint POO_MASTER_POOL_ID_NN
            check ("MASTER_POOL_ID" IS NOT NULL),
    OWNER_GAMBLER_ID RAW(16) not null
        constraint POO_GAMBLER_FK
            references GAMBLER
        constraint POO_OWNER_GAMBLER_ID_NN
            check ("OWNER_GAMBLER_ID" IS NOT NULL),
    CREATION_DATE    DATE    not null
        constraint POO_CREATION_DATE_NN
            check ("CREATION_DATE" IS NOT NULL),
    NAME             VARCHAR2(100)
        constraint POO_NAME_NN
            check (NAME IS NOT NULL)
);
