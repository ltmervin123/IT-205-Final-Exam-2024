-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: May 23, 2024 at 01:14 AM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `final_exam`
--

-- --------------------------------------------------------

--
-- Table structure for table `books`
--

CREATE TABLE `books` (
  `ISBN` int(11) DEFAULT NULL,
  `Title` varchar(100) DEFAULT NULL,
  `author` varchar(100) DEFAULT NULL,
  `publisher` varchar(100) DEFAULT NULL,
  `datePublished` date DEFAULT NULL,
  `category` varchar(100) DEFAULT NULL,
  `numberOfCopies` int(11) DEFAULT NULL,
  `remainingNumberOfCopies` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `books`
--

INSERT INTO `books` (`ISBN`, `Title`, `author`, `publisher`, `datePublished`, `category`, `numberOfCopies`, `remainingNumberOfCopies`) VALUES
(1111, 'Rich Dad', 'Robert Kiyosaki', 'QA Publisher', '2004-12-26', 'Finance Education', 10, 10),
(2222, 'Hello World', 'Robert Lee', 'QA Publisher', '1994-05-26', 'Finance Education', 7, 10),
(3333, 'Lost Ark', 'Alvincent Sangco', 'WT Marketing', '2025-02-22', 'Fiction', 15, 15),
(4444, 'The Greate Wall', 'Lee Mhen Jo', 'China Town Inc', '2025-02-22', 'Drama', 69, 69);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
