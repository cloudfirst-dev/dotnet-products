terraform {
  required_providers {
    aws = {
      source  = "hashicorp/aws"
      version = "~> 3.0"
    }
  }
}

provider "aws" {
    region = "us-east-2"
}

resource "aws_vpc" "dotnet_opa" {
    cidr_block = "10.0.0.0/16"
}

resource "aws_subnet" "dotnet_vm_subnet" {
    vpc_id = aws_vpc.dotnet_opa.id
    cidr_block = "10.0.1.0/24"
    map_public_ip_on_launch = true
}

resource "aws_route_table" "dotnet_vm_public" {
    vpc_id = aws_vpc.dotnet_opa.id
    
    route {
        cidr_block = "0.0.0.0/0"
        gateway_id = aws_internet_gateway.dotnet_vm_gateway.id
    }
}

resource "aws_route_table_association" "dotnet_vm_public" {
    subnet_id = aws_subnet.dotnet_vm_subnet.id
    route_table_id = aws_route_table.dotnet_vm_public.id
}

resource "aws_internet_gateway" "dotnet_vm_gateway" {
    vpc_id = aws_vpc.dotnet_opa.id
}

resource "aws_network_interface" "dotnet_vm" {
    subnet_id = aws_subnet.dotnet_vm_subnet.id
    security_groups = [
        aws_security_group.dotnet_vm.id
    ]
}

resource "aws_security_group" "dotnet_vm" {
    name = "dotnet_vm"
    vpc_id = aws_vpc.dotnet_opa.id

    ingress {
        from_port = 3389
        to_port = 3389
        protocol = "tcp"
        cidr_blocks = ["0.0.0.0/0"]
    }
    ingress {
        from_port = 3389
        to_port = 3389
        protocol = "udp"
        cidr_blocks = ["0.0.0.0/0"]
    }
    ingress {
        from_port = 443
        to_port = 443
        protocol = "tcp"
        cidr_blocks = ["0.0.0.0/0"]
    }
    ingress {
        from_port = 5003
        to_port = 5003
        protocol = "tcp"
        cidr_blocks = ["0.0.0.0/0"]
    }
    ingress {
        from_port = 5005
        to_port = 5005
        protocol = "tcp"
        cidr_blocks = ["0.0.0.0/0"]
    }
    ingress {
        from_port = 3000
        to_port = 3000
        protocol = "tcp"
        cidr_blocks = ["0.0.0.0/0"]
    }
    egress {
        cidr_blocks = ["0.0.0.0/0"]
        from_port = 0
        to_port = 0
        protocol = -1
    }
}

resource "aws_instance" "dotnet_vm" {
    // empty windows vm
    ami = "ami-0f540030bb04d884a"
    // prebuilt ami with all sample binaries and opa ready to run
    # ami = "ami-04800528e193e3f6f"
    instance_type = "t2.small"
    network_interface {
       network_interface_id = aws_network_interface.dotnet_vm.id
       device_index = 0
    }
    key_name = "styra-aws-sa"
}