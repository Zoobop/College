from . import db
from flask_login import UserMixin
from sqlalchemy.sql import func

import enum

class BankAccountType(enum.Enum):
    Stardard = 7500,
    College = 2000,
    Business = 15000

    @staticmethod
    def parse(value: int):
        if value == 1:
            return BankAccountType.College
        elif value == 2:
            return BankAccountType.Business
        return BankAccountType.Stardard

class BankAccount(db.Model):
    id = db.Column(db.Integer, primary_key=True)
    account_type = db.Column(db.Enum(BankAccountType))
    current_funds = db.Column(db.Float)
    user_id = db.Column(db.Integer, db.ForeignKey("user.id"))

class User(db.Model, UserMixin):
    id = db.Column(db.Integer, primary_key=True)
    email = db.Column(db.String(150), unique=True)
    password = db.Column(db.String(150))
    first_name = db.Column(db.String(150))
    last_name = db.Column(db.String(150))
    account = db.relationship("BankAccount")

    def display_type(self):
        return f'{self.account.account_type}'.removeprefix('BankAccountType.')